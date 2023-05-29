using System.Xml.Schema;

namespace CleanTableTennisApp.Domain.Entities;

public class DoubleMatch : AuditableEntity
{
    public DoubleMatch()
    {
        this.Scores = new List<DoubleMatchScore>();
    }
    public int Id { get; set; }
    public Player HostPlayerRight { get; set; }
    public Player HostPlayerLeft { get; set; }
    public Player GuestPlayerRight { get; set; }
    public Player GuestPlayerLeft { get; set; }
    public int TeamMatchId { get; set; }
    public ICollection<DoubleMatchScore> Scores { get; set; }
}