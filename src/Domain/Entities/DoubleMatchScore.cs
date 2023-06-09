namespace CleanTableTennisApp.Domain.Entities;

public class DoubleMatchScore : IGamePoints
{
    public int Id { get; set; }
    public int GamePointsHost { get; set; }
    public int GamePointsGuest { get; set; }
    public DoubleMatch? DoubleMatch { get; set; }
}