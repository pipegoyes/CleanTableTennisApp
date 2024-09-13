using CleanTableTennisApp.Domain.Interfaces;

namespace CleanTableTennisApp.Domain.Entities;

public class SingleMatch : AuditableEntity, IScorable<SingleMatchScore>
{
    public SingleMatch()
    {
        Scores = new List<SingleMatchScore>();
    }

    public SingleMatch(Player hostPlayer, Player guestPlayer, TeamMatch teamMatch, PlayingOrder playingOrder)
    {
        HostPlayer = hostPlayer;
        GuestPlayer = guestPlayer;
        TeamMatch = teamMatch;
        PlayingOrder = playingOrder;
        Scores = new List<SingleMatchScore>();
    }

    public int Id { get; set; }
    public Player HostPlayer { get; set; } = new();
    public Player GuestPlayer { get; set; } = new();
    public TeamMatch TeamMatch { get; set; } = new();
    public PlayingOrder PlayingOrder { get; set; } = new();
    public ICollection<SingleMatchScore> Scores { get; set; }
}
