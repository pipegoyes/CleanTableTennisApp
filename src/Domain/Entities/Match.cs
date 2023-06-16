using System.Xml.Schema;
using CleanTableTennisApp.Domain.Interfaces;

namespace CleanTableTennisApp.Domain.Entities;

public class Match : AuditableEntity, IScorable<Score>
{
    public Match()
    {
        Scores = new List<Score>();
    }

    public Match(Player hostPlayer, Player guestPlayer, TeamMatch teamMatch)
    {
        HostPlayer = hostPlayer;
        GuestPlayer = guestPlayer;
        TeamMatch = teamMatch;
        Scores = new List<Score>();
    }

    public int Id { get; set; }
    public Player HostPlayer { get; set; }
    public Player GuestPlayer { get; set; }
    public TeamMatch TeamMatch { get; set; }
    public short Order { get; set; }
    public ICollection<Score> Scores { get; set; }
}