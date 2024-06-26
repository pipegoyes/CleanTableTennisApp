﻿namespace CleanTableTennisApp.Domain.Entities;

public class TeamMatch : AuditableEntity
{
    // TODO EF needs a parameterless constructor 
    public TeamMatch()
    {
        HostTeam = new Team(string.Empty);
        GuestTeam = new Team(string.Empty);
        SingleMatches = new List<SingleMatch>();
        DoubleMatches = new List<DoubleMatch>();
    }

    public TeamMatch(Team hostTeam, Team guestTeam)
    {
        HostTeam = hostTeam;
        GuestTeam = guestTeam;
        SingleMatches = new List<SingleMatch>();
        DoubleMatches = new List<DoubleMatch>();
    }

    public int Id { get; set; }
    public Team HostTeam { get; set; }
    public Team GuestTeam { get; set; }
    public DateTime? FinishedAt { get; set; }
    public ICollection<SingleMatch> SingleMatches { get; set; }
    public ICollection<DoubleMatch> DoubleMatches { get; set; }
}
