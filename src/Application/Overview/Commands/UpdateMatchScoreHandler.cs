using CleanTableTennisApp.Application.Common.Enconders;
using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Application.Requests;
using CleanTableTennisApp.Domain.Entities;
using FluentValidation;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Overview.Commands;


public class UpdateMatchScoreCommand : IRequest<bool>
{
    public string MatchIdEncoded { get; set; } = string.Empty;
    public string TeamMatchIdEncoded { get; set; } = string.Empty;
    public IEnumerable<ScoreDto> ScoreDtos { get; set; } = Enumerable.Empty<ScoreDto>();
}

public class UpdateMatchScoreHandler : IRequestHandler<UpdateMatchScoreCommand, bool>
{
    private readonly IApplicationDbContext _context;
    private readonly IUrlSafeIntEncoder _encoder;
    private readonly IValidator<ICollection<Score>> _scoreValidator;
    private readonly IGamePointsUpdater<Score> _gamePointsUpdater;
    
    public UpdateMatchScoreHandler(IApplicationDbContext context, 
        IUrlSafeIntEncoder encoder, 
        IValidator<ICollection<Score>> scoreValidator, 
        IGamePointsUpdater<Score> gamePointsUpdater)
    {
        _context = context;
        _encoder = encoder;
        _scoreValidator = scoreValidator;
        _gamePointsUpdater = gamePointsUpdater;
    }

    public async Task<bool> Handle(UpdateMatchScoreCommand request, CancellationToken cancellationToken)
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