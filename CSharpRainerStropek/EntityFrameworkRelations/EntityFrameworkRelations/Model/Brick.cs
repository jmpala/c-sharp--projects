using System.ComponentModel.DataAnnotations;

namespace EntityFrameworkRelations.Model;

public class Brick
{
    public int Id { get; set; }

    [MaxLength(250)]
    public string Title { get; set; } = string.Empty;
    
    public Color? Color { get; set; }

    // Many to many relationship
    public List<Tag> Tags { get; set; }

    public List<BrickAvailability> Availability { get; set; } = new();
}