namespace CleanTableTennisApp.Domain.Entities;

public class TeamMatch : AuditableEntity
{
    public TeamMatch()
    {
    }

    public TeamMatch(Team hostTeam, Team guestTeam)
    {
        HostTeam = hostTeam;
        GuestTeam = guestTeam;
    }

    public int Id { get; set; }
    public Team? HostTeam { get; set; }
    public Team? GuestTeam { get; set; }
    public DateTime? FinishedAt { get; set; }
}