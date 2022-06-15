using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Threading.Tasks;
using TournamentPlanner.Data;

namespace TournamentPlanner.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PlayersController : ControllerBase
{
    private readonly TournamentPlannerDbContext _context;
    
    public PlayersController(TournamentPlannerDbContext context)
    {
        _context = context;
    }

    [HttpGet]
    public async Task<IEnumerable<Player>> Get([FromQuery] string? name = null)
    {
        return await _context.GetFilteredPlayers(name);
    }

    [HttpPost]
    public async Task<Player> AddPlayer([FromBody] Player newPlayer)
    {
        await _context.AddPlayer(newPlayer);
        return newPlayer;
    }
}