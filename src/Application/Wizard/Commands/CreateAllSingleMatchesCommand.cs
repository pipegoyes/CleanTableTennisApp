using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Domain.Entities;
using CleanTableTennisApp.Domain.Enums;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace CleanTableTennisApp.Application.Wizard.Commands;

public class CreateAllSingleMatchesCommand : IRequest<bool>
{
    public int TeamMatchId { get; set; }
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

        AttachSingleMatches(hostPlayers, guestPlayers, teamMatch);



        return await _context.SaveChangesAsync(cancellationToken) > 0;
    }

    private void AttachSingleMatches(Player[] hostPlayers, Player[] guestPlayers, TeamMatch teamMatch)
    {
        // todo think about a more simple way that include order
        int numberOfTuples = hostPlayers.Length / 2;
        for (int i = 0; i < hostPlayers.Length - 1; i += numberOfTuples)
        {
            var firstTuplePlayersHost = new TuplePlayers(hostPlayers[i], hostPlayers[i + 1]);
            var firstTuplePlayersGuest = new TuplePlayers(guestPlayers[i], guestPlayers[i + 1]);
            var singleMatches = CreateMatches(firstTuplePlayersHost, firstTuplePlayersGuest, teamMatch, i);

            foreach (SingleMatch singleMatch in singleMatches)
            {
                teamMatch.SingleMatches.Add(singleMatch);
            }
        }
    }

    private static SingleMatch[] CreateMatches(TuplePlayers hostTuplePlayers, TuplePlayers guestTuplePlayers, TeamMatch teamMatch, int playerPosition)
    {
        var result = new List<SingleMatch>
        {
            new(hostTuplePlayers.FirstPlayer, guestTuplePlayers.SecondPlayer, teamMatch, (PlayingOrder) playerPosition + 3), // +3
            new(hostTuplePlayers.FirstPlayer, guestTuplePlayers.FirstPlayer, teamMatch, (PlayingOrder) playerPosition + 7), // +7

            new(hostTuplePlayers.SecondPlayer, guestTuplePlayers.FirstPlayer, teamMatch, (PlayingOrder) playerPosition + 4), // +4
            new(hostTuplePlayers.SecondPlayer, guestTuplePlayers.SecondPlayer, teamMatch, (PlayingOrder) playerPosition + 8), // +8
        };
        return result.ToArray();
    }
}
