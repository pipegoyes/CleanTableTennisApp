using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Application.Requests;
using CleanTableTennisApp.Domain.Entities;
using CleanTableTennisApp.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Wizard.Commands;

public class CreateDoubleMatchesCommand : IRequest<bool>
{
    public int TeamMatchId { get; set; }
    public IList<DoublePlayerRequest> HostPlayers { get; set; }
    public IList<DoublePlayerRequest> GuestPlayers { get; set; }
}

public class CreateDoubleMatchesHandler : IRequestHandler<CreateDoubleMatchesCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public CreateDoubleMatchesHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(CreateDoubleMatchesCommand request, CancellationToken cancellationToken)
    {
        var teamMatch =  await _context.TeamMatches.SingleAsync(s => s.Id == request.TeamMatchId, cancellationToken);
        
        CreateDoubleMatch(request, teamMatch, DoublePosition.FirstDouble, PlayingOrder.First);
        CreateDoubleMatch(request, teamMatch, DoublePosition.SecondDouble, PlayingOrder.Second);

        _context.TeamMatches.Attach(teamMatch);
        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    private void CreateDoubleMatch(CreateDoubleMatchesCommand request, TeamMatch teamMatch, DoublePosition doublePosition, PlayingOrder playingOrder)
    {
        var hostPlayers = request.HostPlayers.Where(s => s.DoublePosition == doublePosition).ToArray();
        var guestPlayers = request.GuestPlayers.Where(s => s.DoublePosition == doublePosition).ToArray();

        // todo validate that there are exactly two players on it?

        var doubleMatch = new DoubleMatch
        {
            HostPlayerLeft = _context.Players.Single(s => s.Id == hostPlayers[0].PlayerId),
            HostPlayerRight = _context.Players.Single(s => s.Id == hostPlayers[1].PlayerId),
            PlayingOrder = playingOrder,
            GuestPlayerLeft = _context.Players.Single(s => s.Id == guestPlayers[0].PlayerId),
            GuestPlayerRight = _context.Players.Single(s => s.Id == guestPlayers[1].PlayerId),
        };
        teamMatch.DoubleMatches.Add(doubleMatch);
    }
}
