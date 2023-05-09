namespace CleanTableTennisApp.Domain.Entities;

public class Match : AuditableEntity
{
    public Match()
    {
    }

    public Match(TeamMatch teamMatch)
    {
        TeamMatch = teamMatch;
    }

    public int Id { get; set; }
    public Player HostPlayer { get; set; }
    public Player GuestPlayer { get; set; }
    public TeamMatch? TeamMatch { get; set; }
    public short Order { get; set; }
}