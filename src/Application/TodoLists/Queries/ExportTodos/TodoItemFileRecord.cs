using CleanArchitectureSolution.Application.Common.Mappings;
using CleanArchitectureSolution.Domain.Entities;

namespace CleanArchitectureSolution.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
