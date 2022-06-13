using System.ComponentModel.DataAnnotations;
using System.Text.Json.Serialization;

namespace VideoGameManager.DataAccess;

public class GameGenre
{
    public int Id { get; set; }

    [MaxLength(150)]
    [Required]
    public string Name { get; set; } = string.Empty;

    [JsonIgnore]
    public List<Game> Games { get; set; } = new();

}