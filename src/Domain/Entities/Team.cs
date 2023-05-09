namespace CleanTableTennisApp.Domain.Entities;

public class Team : AuditableEntity
{
    public Team(string name)
    {
        Name = name;
        Players = new List<Player>();
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public ICollection<Player> Players { get; set; }
}