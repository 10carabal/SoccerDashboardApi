using FootballStats.Domain.Entities;
using FootballStats.Domain.Interfaces;

namespace FootballStats.Application.UseCases
{
    public class GetTopScorerHandler
    {
        private readonly IFootballApiService _footballApi;

        public GetTopScorerHandler(IFootballApiService footballApi)
        {
            _footballApi = footballApi;
        }

        public async Task<IReadOnlyList<PlayerTopScorer>> HandleAsync(int leagueId, int season)
        {
            return await _footballApi.GetTopScorersAsync(leagueId, season);
        }
    }
}
