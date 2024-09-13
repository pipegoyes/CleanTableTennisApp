using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Common.Encoders;
using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Application.Scores;
using CleanTableTennisApp.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Scores.DoubleMatchScores.Commands;

public class UpdateDoubleMatchScoresCommand : IRequest<bool>
{
    public UpdateDoubleMatchScoresCommand(string doubleMatchIdEncoded)
    {
        DoubleMatchIdEncoded = doubleMatchIdEncoded;
    }

    public string DoubleMatchIdEncoded { get; set; }
    public IEnumerable<ScoreDto> ScoreDtos { get; set; } = Enumerable.Empty<ScoreDto>();
}

public class UpdateDoubleMatchScoresHandler : IRequestHandler<UpdateDoubleMatchScoresCommand, bool>
{
    private readonly IUrlSafeIntEncoder _encoder;
    private readonly IApplicationDbContext _context;
    private readonly IValidator<ICollection<DoubleMatchScore>> _scoreValidator;
    private readonly IGamePointsUpdater<DoubleMatchScore> _gamePointsUpdater;

    public UpdateDoubleMatchScoresHandler(IUrlSafeIntEncoder encoder, IApplicationDbContext context, IValidator<ICollection<DoubleMatchScore>> scoreValidator, IGamePointsUpdater<DoubleMatchScore> gamePointsUpdater)
    {
        _encoder = encoder;
        _context = context;
        _scoreValidator = scoreValidator;
        _gamePointsUpdater = gamePointsUpdater;
    }

    public async Task<bool> Handle(UpdateDoubleMatchScoresCommand request, CancellationToken cancellationToken)
    {
        var doubleMatchIdDecoded = _encoder.Decode(request.DoubleMatchIdEncoded);

        var doubleMatch = await _context.DoubleMatches
            .Include(s => s.Scores)
            .FirstAsync(s => s.Id == doubleMatchIdDecoded, cancellationToken);

        _gamePointsUpdater.UpdateScores(request.ScoreDtos.ToArray(), doubleMatch);

        await _scoreValidator.ValidateAndThrowAsync(doubleMatch.Scores, cancellationToken);

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
