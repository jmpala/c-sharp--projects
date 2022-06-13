using System.ComponentModel.DataAnnotations;

namespace VideoGameManager.DataAccess;

public class Game
{
    public int Id { get; set; }
    
    [MaxLength(150)]
    [Required]
    public string Name { get; set; } = string.Empty;
    
    public GameGenre Genre { get; set; }
    
    public int Rating { get; set; }
}