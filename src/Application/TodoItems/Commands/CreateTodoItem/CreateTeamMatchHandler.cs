using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Application.Requests;
using CleanTableTennisApp.Application.Wizard.Commands;
using CleanTableTennisApp.Domain.Entities;
using MediatR;

namespace CleanTableTennisApp.Application.TodoItems.Commands.CreateTodoItem;

public class CreateTeamMatchHandler : IRequestHandler<CreateTeamMatchCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTeamMatchHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTeamMatchCommand request, CancellationToken cancellationToken)
    {
        var hostTeam = await AddTeamIfNotExists(request.HostTeam.Name, cancellationToken);
        AddPlayers(hostTeam, request.HostTeam.Players);

        var guestTeam = await AddTeamIfNotExists(request.GuestTeam.Name, cancellationToken);
        AddPlayers(guestTeam, request.GuestTeam.Players);

        //var firstDouble = new Match()

        var newTeamMatch = new TeamMatch(hostTeam, guestTeam);
        _context.TeamMatches.Add(newTeamMatch);

        await _context.SaveChangesAsync(cancellationToken);
        return newTeamMatch.Id;
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
