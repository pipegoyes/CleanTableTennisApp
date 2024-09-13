using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Domain.Interfaces;

namespace CleanTableTennisApp.Application.Scores;

public interface IGamePointsUpdater<T> where T : IGamePoints, new()
{
    void UpdateScores(IReadOnlyCollection<ScoreDto> scores, IScorable<T> match);
}
