namespace CleanTableTennisApp.Domain.Entities;

public class Score
{
    public int Id { get; set; }
    public int GamePointsHost { get; set; }
    public int GamePointsGuest { get; set; }
    public Match Match { get; set; }
}