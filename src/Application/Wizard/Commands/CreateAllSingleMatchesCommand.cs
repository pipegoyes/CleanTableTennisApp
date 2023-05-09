using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Wizard.Commands;

public class CreateAllSingleMatchesCommand : IRequest<bool>
{
    public int TeamMatchId { get; set; } = default;
}

public class CreateAllSingleMatchesHandler : IRequestHandler<CreateAllSingleMatchesCommand, bool>
{
    private readonly IApplicationDbContext _context;

    public CreateAllSingleMatchesHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<bool> Handle(CreateAllSingleMatchesCommand request, CancellationToken cancellationToken)
    {
        var teamMatch = await _context.TeamMatches.SingleAsync(s => s.Id == request.TeamMatchId, cancellationToken);
        var hostPlayers = teamMatch.HostTeam.Players.ToArray();
        var guestPlayers = teamMatch.GuestTeam.Players.ToArray();

        CreateSingleMatches(hostPlayers, guestPlayers);

        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    private void CreateSingleMatches(Player[] hostPlayers, Player[] guestPlayers)
    {
        int numberOfTuples = hostPlayers.Length / 2;
        for (int i = 0; i < hostPlayers.Length - 1; i += numberOfTuples)
        {
            var firstTuplePlayersHost = new TuplePlayers(hostPlayers[i], hostPlayers[i + 1]);
            var firstTuplePlayersGuest = new TuplePlayers(guestPlayers[i], guestPlayers[i + 1]);
            var firstTupleMatch = CreateMatches(firstTuplePlayersHost, firstTuplePlayersGuest);

            _context.Matches.AddRange(firstTupleMatch);
        }
    }

    private Match[] CreateMatches(TuplePlayers hostTuplePlayers, TuplePlayers guestTuplePlayers)
    {
        var result = new List<Match>
        {
            new(hostTuplePlayers.FirstPlayer, guestTuplePlayers.SecondPlayer),
            new(hostTuplePlayers.FirstPlayer, guestTuplePlayers.FirstPlayer),

            new(hostTuplePlayers.SecondPlayer, guestTuplePlayers.FirstPlayer),
            new(hostTuplePlayers.SecondPlayer, guestTuplePlayers.SecondPlayer),
        };
        return result.ToArray();
    }
}