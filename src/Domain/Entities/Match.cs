using System.Xml.Schema;

namespace CleanTableTennisApp.Domain.Entities;

public class Match : AuditableEntity
{
    public Match()
    {
    }

    public Match(Player hostPlayer, Player guestPlayer, TeamMatch teamMatch)
    {
        HostPlayer = hostPlayer;
        GuestPlayer = guestPlayer;
        TeamMatch = teamMatch;
    }

    public int Id { get; set; }
    public Player HostPlayer { get; set; }
    public Player GuestPlayer { get; set; }
    public TeamMatch TeamMatch { get; set; }
    public short Order { get; set; }
    public ICollection<Score> Scores { get; set; }
}