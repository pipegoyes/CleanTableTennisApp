namespace CleanTableTennisApp.Domain.Entities;

public class TeamMatch : AuditableEntity
{
    // TODO EF needs a parameterless constructor 
    public TeamMatch()
    {
        Matches = new List<Match>();
    }

    public TeamMatch(Team hostTeam, Team guestTeam)
    {
        HostTeam = hostTeam;
        GuestTeam = guestTeam;
        Matches = new List<Match>();
    }

    public int Id { get; set; }
    public Team HostTeam { get; set; }
    public Team GuestTeam { get; set; }
    public DateTime? FinishedAt { get; set; }
    public ICollection<Match> Matches { get; set; }
}