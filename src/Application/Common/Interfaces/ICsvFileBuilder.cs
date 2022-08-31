using CleanTableTennisApp.Application.TodoLists.Queries.ExportTodos;

namespace CleanTableTennisApp.Application.Common.Interfaces;

public interface ICsvFileBuilder
{
    byte[] BuildTodoItemsFile(IEnumerable<TodoItemRecord> records);
}
