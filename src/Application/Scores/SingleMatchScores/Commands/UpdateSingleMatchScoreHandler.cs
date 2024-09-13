using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Common.Encoders;
using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Application.Scores;
using CleanTableTennisApp.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Scores.SingleMatchScores.Commands;


public class UpdateSingleMatchScoreCommand : IRequest<bool>
{
    public string MatchIdEncoded { get; set; } = string.Empty;
    public string TeamMatchIdEncoded { get; set; } = string.Empty;
    public IEnumerable<ScoreDto> ScoreDtos { get; set; } = Enumerable.Empty<ScoreDto>();
}

public class UpdateSingleMatchScoreHandler : IRequestHandler<UpdateSingleMatchScoreCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IUrlSafeIntEncoder _encoder;
    private readonly IValidator<ICollection<SingleMatchScore>> _scoreValidator;
    private readonly IGamePointsUpdater<SingleMatchScore> _gamePointsUpdater;

    public UpdateSingleMatchScoreHandler(IApplicationDbContext context,
        IUrlSafeIntEncoder encoder,
        IValidator<ICollection<SingleMatchScore>> scoreValidator,
        IGamePointsUpdater<SingleMatchScore> gamePointsUpdater)
    {
        _context = context;
        _encoder = encoder;
        _scoreValidator = scoreValidator;
        _gamePointsUpdater = gamePointsUpdater;
    }

    public async Task<bool> Handle(UpdateSingleMatchScoreCommand request, CancellationToken cancellationToken)
    {
        var matchIdDecoded = _encoder.Decode(request.MatchIdEncoded);
        var match = await _context.SingleMatches
            .Include(s => s.Scores)
            .FirstAsync(s => s.Id == matchIdDecoded, cancellationToken);

        _gamePointsUpdater.UpdateScores(request.ScoreDtos.ToArray(), match);

        await _scoreValidator.ValidateAndThrowAsync(match.Scores, cancellationToken);

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
