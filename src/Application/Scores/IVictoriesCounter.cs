using CleanTableTennisApp.Domain.Interfaces;

namespace CleanTableTennisApp.Application.Scores;

public interface IVictoriesCounter
{
    int HostVictoriesCounter(IReadOnlyCollection<IGamePoints> gamePoints);
    int GuestVictoriesCounter(IReadOnlyCollection<IGamePoints> gamePoints);
}