namespace CleanTableTennisApp.Domain.Entities;

public class Player: AuditableEntity
{
    public Player()
    {
    }

    public Player(string name)
    {
        Name = name;
    }

    public int Id { get; set; }
    public string? Name { get; set; }
    public int TeamId { get; set; }
}