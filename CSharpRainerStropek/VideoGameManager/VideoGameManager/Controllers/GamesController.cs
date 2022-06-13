using System.Diagnostics.CodeAnalysis;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using VideoGameManager.Context;
using VideoGameManager.DataAccess;

namespace VideoGameManager.Controllers;

[ApiController]
[Route("api/[controller]")]
public class GamesController : ControllerBase
{
    private readonly VideoGameContext _context;
    
    public GamesController(VideoGameContext context)
    {
        _context = context;
    }

    [HttpGet]
    public IEnumerable<Game> GetAllGames()
    {
        return _context.Games;
    }

    [HttpGet]
    [Route("{id}")]
    public Game GetGameById(int id)
    {
        return _context.Games.FirstOrDefault(g => g.Id == id);
    }

    [HttpPost]
    public async Task<Game> AddGame([FromBody] Game newGame)
    { 
        _context.Games.Add(newGame);
        await _context.SaveChangesAsync();
        return newGame;
    }
}