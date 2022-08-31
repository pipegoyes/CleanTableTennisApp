using CleanTableTennisApp.Application.Common.Mappings;
using CleanTableTennisApp.Domain.Entities;

namespace CleanTableTennisApp.Application.TodoItems.Queries.GetTodoItemsWithPagination;

public class TodoItemBriefDto : IMapFrom<TodoItem>
{
    public int Id { get; set; }

    public int ListId { get; set; }

    public string? Title { get; set; }

    public bool Done { get; set; }
}
