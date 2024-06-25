using CleanTableTennisApp.Domain.Interfaces;

namespace CleanTableTennisApp.Domain.Entities;

public class DoubleMatch : AuditableEntity, IScorable<DoubleMatchScore>
{
    public DoubleMatch()
    {
        HostPlayerLeft = new Player(string.Empty);
        HostPlayerRight = new Player(string.Empty);
        GuestPlayerLeft = new Player(string.Empty);
        GuestPlayerRight = new Player(string.Empty);
        PlayingOrder = new PlayingOrder();
        this.Scores = new List<DoubleMatchScore>();
    }
    public int Id { get; set; }
    public Player HostPlayerRight { get; set; }
    public Player HostPlayerLeft { get; set; }
    public Player GuestPlayerRight { get; set; }
    public Player GuestPlayerLeft { get; set; }
    public int TeamMatchId { get; set; }
    public PlayingOrder PlayingOrder { get; set; }
    public ICollection<DoubleMatchScore> Scores { get; set; }
}
