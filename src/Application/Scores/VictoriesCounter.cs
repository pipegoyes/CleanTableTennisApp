using CleanTableTennisApp.Domain.Interfaces;

namespace CleanTableTennisApp.Application.Scores;

public class VictoriesCounter : IVictoriesCounter
{
    public int HostVictoriesCounter(IReadOnlyCollection<IGamePoints> gamePoints)
    {
        return gamePoints.Count(c => c.GamePointsHost > c.GamePointsGuest);
    }

    public int GuestVictoriesCounter(IReadOnlyCollection<IGamePoints> gamePoints)
    {
        return gamePoints.Count(c => c.GamePointsGuest > c.GamePointsHost);
    }
}