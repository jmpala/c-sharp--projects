using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkIntro.model;

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