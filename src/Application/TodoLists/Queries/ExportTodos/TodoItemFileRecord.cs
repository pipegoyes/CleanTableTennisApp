using CleanTableTennisApp.Application.Common.Mappings;
using CleanTableTennisApp.Domain.Entities;

namespace CleanTableTennisApp.Application.TodoLists.Queries.ExportTodos;

public class TodoItemRecord : IMapFrom<TodoItem>
{
    public string? Title { get; set; }

    public bool Done { get; set; }
}
