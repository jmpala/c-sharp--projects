using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using TournamentPlanner.Data;

namespace TournamentPlanner.Controllers;

[Route("api/[controller]")]
[ApiController]
public class MatchesController : ControllerBase
{
    private readonly TournamentPlannerDbContext _context;

    public MatchesController(TournamentPlannerDbContext context)
    {
        _context = context;
    }

    [HttpPost("generate")]
    public async Task GenerateRound()
    {
        await _context.GenerateMatchesForNextRound();
    }

    [HttpGet("open")]
    public async Task<IEnumerable<Match>> GetIncompleteMatches()
    {
        return await _context.GetIncompleteMatches();
    }
}