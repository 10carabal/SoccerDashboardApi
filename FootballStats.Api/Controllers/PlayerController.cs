using FootballStats.Application.UseCases;
using Microsoft.AspNetCore.Mvc;

namespace FootballStats.Api.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PlayerController : ControllerBase
    {
        private readonly GetTopScorerHandler _handler;

        public PlayerController(GetTopScorerHandler handler)
        {
            _handler = handler;
        }

        [HttpGet("topscorers")]
        public async Task<IActionResult> GetTopScorers([FromQuery] int leagueId, [FromQuery] int season)
        {
            var result = await _handler.HandleAsync(leagueId, season);
            return Ok(result);
        }
    }
}
