using System.Linq.Expressions;
using EntityFrameworkIntro.context;
using EntityFrameworkIntro.model;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

public static class Program
{
    public static async Task Main(string[] args)
    {
        var factory = new CookbookContextFactory();
        using var dbcontext = factory.CreateDbContext(args);

        var newDish = new Dish
        {
            Title = "Foo",
            Notes = "Bar"
        };

        dbcontext.Dishes.Add(newDish);
        await dbcontext.SaveChangesAsync();
        newDish.Notes = "Baz";
        await dbcontext.SaveChangesAsync();

        await EntityStates(factory, args);
        await ChangeTracking(factory, args);
        await AttachEntities(factory, args);
        await NoTracking(factory, args);
        await RawSql(factory, args);
        await Transactions(factory, args);
        await ExpressionTree(factory, args);
    }

    static async Task ExpressionTree(CookbookContextFactory factory, string[] args)
    {
        using var dbContext = factory.CreateDbContext(args);
        
        var newDish = new Dish { Title = "Foo",Notes = "Bar" };
        dbContext.Add(newDish);
        await dbContext.SaveChangesAsync();

        var dishes = await dbContext.Dishes
            .Where(d => d.Title.StartsWith("F")) // Lambda expression
            .ToArrayAsync();

        Func<Dish, bool> f = d => d.Title.StartsWith("F");
        Expression<Func<Dish, bool>> ex = d => d.Title.StartsWith("F");
    }
    
    static async Task Transactions(CookbookContextFactory factory, string[] args)
    {
        using var dbContext = factory.CreateDbContext(args);

        // Starting the transaction
        using var transaction = await dbContext.Database.BeginTransactionAsync();
        try
        {
            var newDish = new Dish { Title = "Foo",Notes = "Bar" };
            dbContext.Add(newDish);
            await dbContext.SaveChangesAsync();

            await dbContext.Database.ExecuteSqlRawAsync("SELECT 1/0 as Bad");
            await transaction.CommitAsync(); // Finishing the transaction
        }
        catch (SqlException ex)
        {
            Console.Error.WriteLine(ex.Message);
        }
        
    }
    
    static async Task RawSql(CookbookContextFactory factory, string[] args)
    {
        using var dbContext = factory.CreateDbContext(args);
        
        // Create a static Query
        var dishes = await dbContext.Dishes
            .FromSqlRaw("SELECT * FROM Dishes")
            .ToArrayAsync();

        // Create the Query with parameters
        var filter = "%z";
        dishes = await dbContext.Dishes
            .FromSqlInterpolated($"SELECT * FROM Dishes WHERE Notes LIKE {filter}")
            .ToArrayAsync();
        
        // Bad Code - SQLInjection sample
        // dishes = await dbContext.Dishes
        //     .FromSqlRaw("SELECT * FROM Dishes WHERE Notes LIKE '" + filter + "'")
        //     .ToArrayAsync();

        
        await dbContext.Database.ExecuteSqlRawAsync("DELETE FROM Dishes WHERE Id NOT IN (SELECT DishId FROM Ingredients)");
    }
    
    static async Task NoTracking(CookbookContextFactory factory, string[] args)
    {
        using var dbContext = factory.CreateDbContext(args);

        // SELECT * FROM Dishes
        var dishes = await dbContext.Dishes.AsNoTracking().ToArrayAsync();
        var state = dbContext.Entry(dishes[0]).State; // << Detached - not being tracked
    }
    
    static async Task AttachEntities(CookbookContextFactory factory, string[] args)
    {
        using var dbContext = factory.CreateDbContext(args);
        
        var newDish = new Dish { Title = "Foo",Notes = "Bar" };
        dbContext.Dishes.Add(newDish);
        await dbContext.SaveChangesAsync();

        dbContext.Entry(newDish).State = EntityState.Detached; // we can set states
        var state = dbContext.Entry(newDish).State;

        dbContext.Update(newDish);
        await dbContext.SaveChangesAsync();
    }
    
    static async Task ChangeTracking(CookbookContextFactory factory, string[] args)
    {
        using var dbContext = factory.CreateDbContext(args);
        
        var newDish = new Dish { Title = "Foo",Notes = "Bar" };
        dbContext.Dishes.Add(newDish);
        await dbContext.SaveChangesAsync(); // Compares original value with neu if exist
        newDish.Notes = "Baz";

        var entry = dbContext.Entry(newDish);
        var originalValue = entry.OriginalValues[nameof(Dish.Notes)].ToString();
        var dishFromDb = await dbContext.Dishes.SingleAsync(d => d.Id == newDish.Id);
        
        // ----

        using var dbContext2 = factory.CreateDbContext(args);
        var dishFromDb2 = await dbContext2.Dishes.SingleAsync(d => d.Id == newDish.Id);
        
    }
    
    static async Task EntityStates(CookbookContextFactory factory,string[] args)
    {
        using var dbContext = factory.CreateDbContext(args);

        var newDish = new Dish { Title = "Foo",Notes = "Bar" };
        var detachedState = dbContext.Entry(newDish).State; // << Detached

        dbContext.Dishes.Add(newDish);
        var addedState = dbContext.Entry(newDish).State; // << Added

        await dbContext.SaveChangesAsync();
        var unchangedState = dbContext.Entry(newDish).State; // << Unchanged

        newDish.Notes = "Baz";
        var modifiedState = dbContext.Entry(newDish).State; // << Modified

        dbContext.Remove(newDish);
        var deleteState = dbContext.Entry(newDish).State; // << Deleted

        await dbContext.SaveChangesAsync();
        var state = dbContext.Entry(newDish).State; // << Detached
    }
}









