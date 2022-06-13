namespace EntityFrameworkRelations.Model;

public class Tag
{
    public int Id { get; set; }

    public string Title { get; set; }
    
    // Many to many relationship
    public List<Brick> Bricks { get; set; }
}