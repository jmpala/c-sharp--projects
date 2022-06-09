using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var factory = new CookbookContextFactory();
        using var context = factory.CreateDbContext(args);

        Console.WriteLine("Add Porridge for breakfast");
        var porridge = new Dish
        {
            Title = "Breakfast Porridge",
            Notes = "This is good",
            Starts = 4
        };
        context.Dishes.Add(porridge);
        await context.SaveChangesAsync(); // important
        Console.WriteLine("Add Porridge successfully");

        porridge.Starts = 69;
        await context.SaveChangesAsync(); // important
        
        Console.WriteLine("Total stars porridge");
        var dishes = await context.Dishes
            .Where(d => d.Title.Contains("Porridge"))
            .ToListAsync();
        
        Console.WriteLine($"Total stars: {dishes[0].Starts}");
        Console.ReadKey();
        
        Console.WriteLine("Remove Porridge for DB");
        context.Dishes.Remove(porridge);
        await context.SaveChangesAsync(); // important
        Console.WriteLine("Porridge removed");
    }
}


// Create the model class
// model -> table
class Dish
{
    public int Id { get; set; }
    
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(1000)]
    public string? Notes { get; set; }
    
    public int Starts { get; set; }

    public List<Ingredient> Ingredients { get; set; } = new();
}

class Ingredient
{
    public int Id { get; set; }

    [MaxLength(100)]
    public string Description { get; set; } = string.Empty;

    [MaxLength(50)]
    public string UnitOfMeasure { get; set; } = string.Empty;
    
    [Column(TypeName = "decimal(5,2)")]
    public decimal Amount { get; set; }

    public Dish? Dish { get; set; }

    public int DishId { get; set; }
}

class something
{
    public int Id { get; set; }

    public string name { get; set; }
}

class CookbookContext : DbContext
{
    public DbSet<Dish> Dishes { get; set; }

    public DbSet<Ingredient> Ingredients { get; set; }
    
    public DbSet<something> Somethings { get; set; }

    public CookbookContext(DbContextOptions<CookbookContext> options) : base(options)
    {
        
    }
    
    
}

class CookbookContextFactory : IDesignTimeDbContextFactory<CookbookContext>
{
    public CookbookContext CreateDbContext(string[] args)
    {
        var configuration = new ConfigurationBuilder().AddJsonFile("appsettings.json").Build();

        var optionsBuilder = new DbContextOptionsBuilder<CookbookContext>();
        optionsBuilder
            // Uncomment the following line if you want to print generated
            // SQL statements on the console.
            // .UseLoggerFactory(LoggerFactory.Create(builder => builder.AddConsole()))
            .UseSqlServer(configuration["ConnectionStrings:DefaultConnection"]);

        return new CookbookContext(optionsBuilder.Options);
    }
}