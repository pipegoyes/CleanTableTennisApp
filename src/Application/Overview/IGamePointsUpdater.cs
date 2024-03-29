using CleanTableTennisApp.Application.Requests;
using CleanTableTennisApp.Domain.Interfaces;

namespace CleanTableTennisApp.Application.Overview;

public interface IGamePointsUpdater<T> where T : IGamePoints, new()
{
    void UpdateScores(IReadOnlyCollection<ScoreDto> scores, IScorable<T> match);
}