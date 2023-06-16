namespace CleanTableTennisApp.Domain.Interfaces;

public interface IScorable<T> where T : IGamePoints
{
    public ICollection<T> Scores { get; set; }
}