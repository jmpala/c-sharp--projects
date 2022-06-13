using Microsoft.EntityFrameworkCore;
using VideoGameManager.DataAccess;

namespace VideoGameManager.Context;

public class VideoGameContext : DbContext
{
    public VideoGameContext(DbContextOptions options) : base(options)
    {
        
    }

    public DbSet<Game> Games { get; set; }
    
    public DbSet<GameGenre> Genres { get; set; }
}