using CleanTableTennisApp.Application.Common.Encoders;
using CleanTableTennisApp.Application.Common.Interfaces;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Overview.Commands;


public class FinishTeamMatchCommand : IRequest<bool>
{
    public string TeamMatchIdEncoded { get; set; } = string.Empty;
}

public class FinishTeamMatchHandler : IRequestHandler<FinishTeamMatchCommand, bool>
{
    private readonly IUrlSafeIntEncoder _encoder;
    private readonly IApplicationDbContext _context;
    private readonly IDateTime _dateTime;

    public FinishTeamMatchHandler(IUrlSafeIntEncoder encoder, IApplicationDbContext context, IDateTime dateTime)
    {
        _encoder = encoder;
        _context = context;
        _dateTime = dateTime;
    }

    public async Task<bool> Handle(FinishTeamMatchCommand request, CancellationToken cancellationToken)
    {
        var teamMatchId = _encoder.Decode(request.TeamMatchIdEncoded);
        var teamMatch = await _context.TeamMatches.FirstAsync(s => s.Id == teamMatchId, cancellationToken);
        teamMatch.FinishedAt = _dateTime.Now;
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }
}
