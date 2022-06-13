
using EntityFrameworkRelations.Context;
using EntityFrameworkRelations.Model;
using Microsoft.EntityFrameworkCore;

public class Program
{
    public static async Task Main(string[] args)
    {
        var factory = new BrickContextFactory();
        using var context = factory.CreateDbContext(args);

        // await AddData(context);
        // await QueryData(context);
        await SpecialQueryData(context);
        Console.WriteLine("Done!");
    }

    static async Task QueryData(BrickContext context)
    {
        var availabilityData = await context.BrickAvailabilities
            .Include(ba => ba.Brick)
            .Include(ba => ba.Vendor)
            .ToArrayAsync();

        foreach (var item in availabilityData)
        {
            Console.WriteLine($"{item.Brick.Title} - {item.Vendor.VendorName} - {item.PriceEur}");
        }

        
        var brickWithVendorsAndTags = await context.Bricks
            .Include(nameof(Brick.Availability) + "." + nameof(BrickAvailability.Vendor))
            .Include(b => b.Tags)
            .ToArrayAsync();

        foreach (var item in brickWithVendorsAndTags)
        {
            Console.WriteLine($"{item.Title}");
            if (item.Tags.Any())
            {
                Console.WriteLine($"{string.Join(',', item.Tags.Select(t => t.Title))}");
            }

            if (item.Availability.Any())
            {
                Console.WriteLine($"{string.Join(',', item.Availability.Select(a => a.Vendor.VendorName))}");
            }
        }
    }

    static async Task SpecialQueryData(BrickContext context)
    {
        var simpleBricks = await context.Bricks.ToArrayAsync();
        foreach (var item in simpleBricks)
        {
            await context.Entry(item).Collection(i => i.Tags).LoadAsync(); // Important
            Console.WriteLine($"Title: {item.Title}");
            if (item.Tags.Any())
            {
                Console.WriteLine($"{string.Join(',', item.Tags.Select(t => t.Title))}");
            }
        }
    }
    
    static async Task AddData(BrickContext context)
    { 
        Vendor brickKing, heldDerSteine;
        await context.Vendors.AddRangeAsync(new[]
        {
            brickKing = new Vendor()
            {
                VendorName = "Brick King"
            },
            heldDerSteine = new Vendor()
            {
                VendorName = "Held der Steine"
            },
        });
        await context.SaveChangesAsync();

        Tag rare, ninjago, minecraft;
        await context.Tags.AddRangeAsync(new[]
        {
            rare = new Tag()
            {
                Title = "Rare"
            },
            ninjago = new Tag()
            {
                Title = "Ninjago"
            },
            minecraft = new Tag()
            {
                Title = "Minecraft"
            },
        });
        await context.SaveChangesAsync();

        await context.AddAsync(new BasePlate()
        {
            Title = "BasePlate 16 x 16 with blue water pattern",
            Color = Color.Green,
            Tags = new()
            {
                rare,
                minecraft
            },
            Length = 16,
            Width = 16,
            Availability = new()
            {
                new()
                {
                    Vendor = brickKing,
                    AvailableAmount = 5,
                    PriceEur = 6.5m
                },
                new()
                {
                    Vendor = heldDerSteine,
                    AvailableAmount = 10,
                    PriceEur = 5.9m
                },
            }
        });
        await context.SaveChangesAsync();
    }
}