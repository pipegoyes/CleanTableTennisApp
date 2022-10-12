using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Domain.Entities;
using CleanTableTennisApp.Domain.Events;
using MediatR;

namespace CleanTableTennisApp.Application.TodoItems.Commands.CreateTodoItem;

public enum DoublePosition
{
    None,
    FirstDouble,
    SecondDouble
}

public class PlayerRequest
{
    public string? FullName { get; set; }
    public DoublePosition DoublePosition { get; set; }
}

public class TeamRequest
{
    public string? Name { get; set; }
    public IList<PlayerRequest> Players { get; set; } = new List<PlayerRequest>();
}

public class CreateTeamMatchCommand : IRequest<int>
{
    public TeamRequest HostTeam { get; set; } = new TeamRequest();
    public TeamRequest GuestTeam { get; set; } = new TeamRequest();
}

public class CreateTeamMatchHandler : IRequestHandler<CreateTeamMatchCommand, int>
{
    public Task<int> Handle(CreateTeamMatchCommand request, CancellationToken cancellationToken)
    {
        // request.HostTeam.Name
        // todo requests objects could be more strict with data (avoid nullability)

        throw new NotImplementedException();
    }
}

public class CreateTodoItemCommand : IRequest<int>
{
    public int ListId { get; set; }

    public string? Title { get; set; }
}

public class CreateTodoItemCommandHandler : IRequestHandler<CreateTodoItemCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoItemCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoItem
        {
            ListId = request.ListId,
            Title = request.Title,
            Done = false
        };

        entity.DomainEvents.Add(new TodoItemCreatedEvent(entity));

        _context.TodoItems.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
