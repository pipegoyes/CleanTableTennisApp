using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Application.Requests;
using CleanTableTennisApp.Domain.Entities;
using CleanTableTennisApp.Domain.Enums;
using MediatR;

namespace CleanTableTennisApp.Application.Wizard.Commands;

public class CreateTeamMatchCommand : IRequest<int>
{
    public TeamRequest HostTeam { get; set; } = new();
    public TeamRequest GuestTeam { get; set; } = new();
}

public class CreateTeamMatchHandler : IRequestHandler<CreateTeamMatchCommand, int>
{
    private readonly IApplicationDbContext _context;
    private readonly IMediator _mediator;

    public CreateTeamMatchHandler(IApplicationDbContext context, IMediator mediator)
    {
        _context = context;
        _mediator = mediator;
    }

    public async Task<int> Handle(CreateTeamMatchCommand request, CancellationToken cancellationToken)
    {
        var hostTeam = await AddTeamIfNotExists(request.HostTeam.Name, cancellationToken);
        AddPlayers(hostTeam, request.HostTeam.Players);

        var guestTeam = await AddTeamIfNotExists(request.GuestTeam.Name, cancellationToken);
        AddPlayers(guestTeam, request.GuestTeam.Players);

        var teamMatch = new TeamMatch(hostTeam, guestTeam);
        _context.TeamMatches.Add(teamMatch);

        await _context.SaveChangesAsync(cancellationToken);

        var allMatchesCreated = await _mediator.Send(new CreateAllSingleMatchesCommand { TeamMatchId = teamMatch.Id }, cancellationToken);

        var guestNamesWithDoublePosition = request.GuestTeam.Players.ToDictionary(s => s.FullName, r => r.DoublePosition);
        var hostNamesWithDoublePosition = request.HostTeam.Players.ToDictionary(s => s.FullName, r => r.DoublePosition);

        var createDoubleMatchesCommand = new CreateDoubleMatchesCommand
        {
            TeamMatchId = teamMatch.Id,
            GuestPlayers = ToDoublePlayerRequests(guestTeam.Players, guestNamesWithDoublePosition),
            HostPlayers = ToDoublePlayerRequests(hostTeam.Players, hostNamesWithDoublePosition)
        };
        var allDoubleMatchesCreated = await _mediator.Send(createDoubleMatchesCommand, cancellationToken);

        //todo what do to with responses ?

        return teamMatch.Id;
    }

    private IList<DoublePlayerRequest> ToDoublePlayerRequests(ICollection<Player> players, IDictionary<string, DoublePosition> namesWithDoublePosition)
    {
        return players.Select(s => new DoublePlayerRequest { DoublePosition = namesWithDoublePosition[s.Name], PlayerId = s.Id }).ToArray();
    }

    private static void AddPlayers(Team hostTeam, IList<PlayerRequest> players)
    {
        foreach (PlayerRequest player in players)
        {
            if (hostTeam.Players.All(s => s.Name != player.FullName))
            {
                hostTeam.Players.Add(new Player(player.FullName));
            }
        }
    }

    private async Task<Team> AddTeamIfNotExists(string teamName, CancellationToken cancellationToken)
    {
        var existingHost = _context.Teams.SingleOrDefault(s => s.Name == teamName);
        if (existingHost == null)
        {
            existingHost = new Team(teamName);
            existingHost = _context.Teams.Add(existingHost).Entity;
            await _context.SaveChangesAsync(cancellationToken);
        }

        return existingHost;
    }
}