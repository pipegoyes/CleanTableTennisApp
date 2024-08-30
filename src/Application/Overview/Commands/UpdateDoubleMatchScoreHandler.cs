using CleanTableTennisApp.Application.Common.Dtos;
using CleanTableTennisApp.Application.Common.Enconders;
using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Overview.Commands;

public class UpdateDoubleMatchScoreCommand : IRequest<bool>
{
    public UpdateDoubleMatchScoreCommand(string doubleMatchIdEncoded)
    {
        this.DoubleMatchIdEncoded = doubleMatchIdEncoded;
    }

    public string DoubleMatchIdEncoded { get; set; }
    public IEnumerable<ScoreDto> ScoreDtos { get; set; } = Enumerable.Empty<ScoreDto>();
}

public class UpdateDoubleMatchScoreHandler : IRequestHandler<UpdateDoubleMatchScoreCommand, bool>
{
    private readonly IUrlSafeIntEncoder _encoder;
    private readonly IApplicationDbContext _context;
    private readonly IValidator<ICollection<DoubleMatchScore>> _scoreValidator;
    private readonly IGamePointsUpdater<DoubleMatchScore> _gamePointsUpdater;

    public UpdateDoubleMatchScoreHandler(IUrlSafeIntEncoder encoder, IApplicationDbContext context, IValidator<ICollection<DoubleMatchScore>> scoreValidator, IGamePointsUpdater<DoubleMatchScore> gamePointsUpdater)
    {
        _encoder = encoder;
        _context = context;
        _scoreValidator = scoreValidator;
        _gamePointsUpdater = gamePointsUpdater;
    }

    public async Task<bool> Handle(UpdateDoubleMatchScoreCommand request, CancellationToken cancellationToken)
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