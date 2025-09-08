using FootballStats.Domain.Entities;

namespace FootballStats.Domain.Interfaces
{
    public interface IFootballApiService
    {
         Task<IReadOnlyList<PlayerTopScorer>> GetTopScorersAsync(int leagueId, int season);
    }
}
