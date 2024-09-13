using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Common.Encoders;
using CleanTableTennisApp.Domain.Extensions;
using CleanTableTennisApp.Domain.Interfaces;

namespace CleanTableTennisApp.Application.Scores;

public class GamePointsUpdater<T> : IGamePointsUpdater<T> where T : IGamePoints, new()
{
    private readonly IUrlSafeIntEncoder _encoder;

    public GamePointsUpdater(IUrlSafeIntEncoder encoder)
    {
        _encoder = encoder;
    }

    public void UpdateScores(IReadOnlyCollection<ScoreDto> scores, IScorable<T> match)
    {
        var (newScores, updatedScores) = scores.Split(s => s.ScoreIdEncoded.IsNullOrWhiteSpace());
        foreach (var updatedScore in updatedScores)
        {
            var decodedId = _encoder.Decode(updatedScore.ScoreIdEncoded);
            var score = match.Scores.First(s => s.Id == decodedId);

            score.GamePointsGuest = updatedScore.GuestPoints;
            score.GamePointsHost = updatedScore.HostPoints;
        }

        foreach (var newScore in newScores)
        {
            match.Scores.Add(new T
            {
                GamePointsGuest = newScore.GuestPoints,
                GamePointsHost = newScore.HostPoints
            });
        }
    }
}
