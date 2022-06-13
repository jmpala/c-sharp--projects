using EntityFrameworkRelations.Model;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkRelations.Context;

public class BrickContext : DbContext
{
    public BrickContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Brick> Bricks { get; set; }
    
    public DbSet<Vendor> Vendors { get; set; }
    
    public DbSet<BrickAvailability> BrickAvailabilities { get; set; }
    
    public DbSet<Tag> Tags { get; set; }

    // We are telling EF to store derived classes
    // on the same table as parent class
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<BasePlate>().HasBaseType<Brick>();
        modelBuilder.Entity<MiniFigHead>().HasBaseType<Brick>();
    }
}