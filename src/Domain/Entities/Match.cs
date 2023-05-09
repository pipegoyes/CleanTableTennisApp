namespace CleanTableTennisApp.Domain.Entities;

public class Match : AuditableEntity
{
    public Match()
    {
    }

    public Match(Player hostPlayer, Player guestPlayer, short order)
    {
        HostPlayer = hostPlayer;
        GuestPlayer = guestPlayer;
        Order = order;
    }

    public Match(Player hostPlayer, Player guestPlayer)
    {
        HostPlayer = hostPlayer;
        GuestPlayer = guestPlayer;
    }

    public int Id { get; set; }
    public Player HostPlayer { get; set; }
    public Player GuestPlayer { get; set; }
    public TeamMatch? TeamMatch { get; set; }
    public short Order { get; set; }
}