using CleanTableTennisApp.Application.TeamMatch.Commands;
using MediatR;

namespace CleanTableTennisApp.Application.TodoItems.Commands.CreateTodoItem;

public class CreateTeamMatchHandler : IRequestHandler<CreateTeamMatchCommand, int>
{
    public Task<int> Handle(CreateTeamMatchCommand request, CancellationToken cancellationToken)
    {
        // request.HostTeam.Name
        // todo requests objects could be more strict with data (avoid nullability)

        throw new NotImplementedException();
    }
}
