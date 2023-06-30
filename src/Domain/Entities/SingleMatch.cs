using System.Xml.Schema;
using CleanTableTennisApp.Domain.Interfaces;

namespace CleanTableTennisApp.Domain.Entities;

public class SingleMatch : AuditableEntity, IScorable<Score>
{
    public SingleMatch()
    {
        Scores = new List<Score>();
    }

    public SingleMatch(Player hostPlayer, Player guestPlayer, TeamMatch teamMatch, PlayingOrder playingOrder)
    {
        HostPlayer = hostPlayer;
        GuestPlayer = guestPlayer;
        TeamMatch = teamMatch;
        PlayingOrder = playingOrder;
        Scores = new List<Score>();
    }

    public int Id { get; set; }
    public Player HostPlayer { get; set; }
    public Player GuestPlayer { get; set; }
    public TeamMatch TeamMatch { get; set; }
    public PlayingOrder PlayingOrder { get; set; }
    public ICollection<Score> Scores { get; set; }
}