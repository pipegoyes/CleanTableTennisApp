namespace CleanTableTennisApp.Domain.Entities;

public class DoubleMatch : AuditableEntity
{
    public int Id { get; set; }
    public Player HostPlayerRight { get; set; }
    public Player HostPlayerLeft { get; set; }
    public Player GuestPlayerRigth { get; set; }
    public Player GuestPlayerLeft { get; set; }
    public int TeamMatchId { get; set; }
}