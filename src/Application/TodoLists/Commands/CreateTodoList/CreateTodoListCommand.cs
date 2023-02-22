﻿using CleanTableTennisApp.Application.Common.Interfaces;
using CleanTableTennisApp.Domain.Entities;
using MediatR;

namespace CleanTableTennisApp.Application.TodoLists.Commands.CreateTodoList;

public class CreateTodoListCommand : IRequest<int>
{
    public string? Title { get; set; }
}

public class CreateTodoListCommandHandler : IRequestHandler<CreateTodoListCommand, int>
{
    private readonly IApplicationDbContext _context;

    public CreateTodoListCommandHandler(IApplicationDbContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateTodoListCommand request, CancellationToken cancellationToken)
    {
        var entity = new TodoList { Title = request.Title };

        _context.TodoLists.Add(entity);

        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
