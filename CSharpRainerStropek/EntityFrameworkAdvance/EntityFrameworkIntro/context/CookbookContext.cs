using EntityFrameworkIntro.model;
using Microsoft.EntityFrameworkCore;

namespace EntityFrameworkIntro.context;

class CookbookContext : DbContext
{
    public DbSet<Dish> Dishes { get; set; }

    public DbSet<Ingredient> Ingredients { get; set; }
    
    public CookbookContext(DbContextOptions<CookbookContext> options) : base(options)
    {
        
    }
}