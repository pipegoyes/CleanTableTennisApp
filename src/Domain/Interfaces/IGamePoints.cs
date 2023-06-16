namespace CleanTableTennisApp.Domain.Interfaces;

public interface IGamePoints
{
    int GamePointsHost { get; set; }
    int GamePointsGuest { get; set; }
    int Id { get; set; }
}