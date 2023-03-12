using CleanTableTennisApp.Application.Requests;
using MediatR;

namespace CleanTableTennisApp.Application.TeamMatch.Commands;

public class CreateTeamMatchCommand : IRequest<int>
{
    public TeamRequest HostTeam { get; set; } = new();
    public TeamRequest GuestTeam { get; set; } = new();
}