using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Application.TeamMatch.Commands;
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
        // request.HostTeam.Name
        // todo requests objects could be more strict with data (avoid nullability)

        var hostTeam = await AddTeamIfNotExists(request.HostTeam.Name, cancellationToken); 
        var guestTeam = await AddTeamIfNotExists(request.GuestTeam.Name, cancellationToken);
        var newTeamMatch = new Domain.Entities.TeamMatch(hostTeam, guestTeam);
        _context.TeamMatches.Add(newTeamMatch);
        await _context.SaveChangesAsync(cancellationToken);
        return newTeamMatch.Id;
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
