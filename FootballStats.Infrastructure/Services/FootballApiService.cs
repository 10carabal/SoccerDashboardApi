using FootballStats.Domain.Entities;
using FootballStats.Domain.Interfaces;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace FootballStats.Infrastructure.Services
{
    public class FootballApiService : IFootballApiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        private readonly IConfiguration _config;
        private readonly ILogger<FootballApiService> _logger;

        public FootballApiService(HttpClient httpClient, IConfiguration config, ILogger<FootballApiService> logger)
        {
            _httpClient = httpClient;
            _config = config;
            _logger = logger;

            _apiKey = config["ApiFootball:Key"] ?? throw new ArgumentNullException("ApiFootball Key not found");
            var host = config["ApiFootball:Host"] ?? throw new ArgumentNullException("ApiFootball Host not found");

            // BaseAddress con el host exacto (no añadas esquema en appsettings)
            _httpClient.BaseAddress = new Uri($"https://{host}");

            // Limpia headers antiguos
            _httpClient.DefaultRequestHeaders.Remove("X-RapidAPI-Key");
            _httpClient.DefaultRequestHeaders.Remove("X-RapidAPI-Host");
            _httpClient.DefaultRequestHeaders.Remove("x-apisports-key");
            _httpClient.DefaultRequestHeaders.Remove("Accept");

            // Detecta proveedor por el host y aplica header correcto (no mezclar RapidAPI y api-sports)
            if (host.Contains("rapidapi", StringComparison.OrdinalIgnoreCase))
            {
                _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", _apiKey);
                _httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Host", host);
            }
            else
            {
                // Para api-sports (acceso directo)
                _httpClient.DefaultRequestHeaders.Add("x-apisports-key", _apiKey);
            }

            _httpClient.DefaultRequestHeaders.Add("Accept", "application/json");
        }

        public async Task<IReadOnlyList<PlayerTopScorer>> GetTopScorersAsync(int leagueId, int season)
        {
            var url = $"/players/topscorers?league={leagueId}&season={season}";
            var request = new HttpRequestMessage(HttpMethod.Get, url);

            var response = await _httpClient.SendAsync(request);
            var raw = await response.Content.ReadAsStringAsync();

            // Loguea la respuesta cruda para diagnosis
            _logger.LogDebug("Football API raw response (status {StatusCode}): {Raw}", (int)response.StatusCode, raw);

            if (!response.IsSuccessStatusCode)
            {
                throw new HttpRequestException($"Error calling Football API: {(int)response.StatusCode} {response.ReasonPhrase}. Body: {raw}");
            }

            // Deserializa manualmente desde el JSON crudo para evitar leer el stream dos veces
            ApiFootballResponse? json;
            try
            {
                json = JsonSerializer.Deserialize<ApiFootballResponse>(raw, new JsonSerializerOptions
                {
                    PropertyNameCaseInsensitive = true
                });
            }
            catch (JsonException jex)
            {
                _logger.LogWarning(jex, "Error deserializando respuesta de Football API. Raw: {Raw}", raw);
                return new List<PlayerTopScorer>();
            }

            // Si la respuesta viene vacía, loguea detalles para que puedas ver "results", "errors" u otros campos
            if (json == null || json.Response == null || !json.Response.Any())
            {
                _logger.LogInformation("Football API returned 200 but no data. Raw: {Raw}", raw);
                return new List<PlayerTopScorer>();
            }

            return json.Response.Select(p => new PlayerTopScorer
            {
                Name = p.Player?.Name ?? string.Empty,
                Team = p.Statistics?.FirstOrDefault()?.Team?.Name ?? string.Empty,
                Goals = p.Statistics?.FirstOrDefault()?.Goals?.Total ?? 0,
                Assists = p.Statistics?.FirstOrDefault()?.Goals?.Assists ?? 0,
                PhotoUrl = p.Player?.Photo ?? string.Empty
            }).ToList();
        }

    }
    // Dtos internos para mapear la respuesta
    internal class ApiFootballResponse
    {
        public List<ApiFootballPlayerResponse> Response { get; set; } = new();
        public int Results { get; set; }
        // Use JsonElement para aceptar array, objeto o null sin fallar la deserialización
        public JsonElement Errors { get; set; }
    }

    internal class ApiFootballPlayerResponse
    {
        public ApiFootballPlayer Player { get; set; } = new();
        public List<ApiFootballStatistic> Statistics { get; set; } = new();
    }

    internal class ApiFootballPlayer
    {
        public string Name { get; set; } = string.Empty;
        public string Photo { get; set; } = string.Empty;
    }

    internal class ApiFootballStatistic
    {
        public ApiFootballTeam Team { get; set; } = new();
        public ApiFootballGoals Goals { get; set; } = new();
    }

    internal class ApiFootballTeam
    {
        public string Name { get; set; } = string.Empty;
    }

    internal class ApiFootballGoals
    {
        public int? Total { get; set; }
        public int? Assists { get; set; }
    }
}
