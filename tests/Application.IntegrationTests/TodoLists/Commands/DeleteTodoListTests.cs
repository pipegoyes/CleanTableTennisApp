using CleanArchitectureSolution.Application.Common.Exceptions;
using CleanArchitectureSolution.Application.TodoLists.Commands.CreateTodoList;
using CleanArchitectureSolution.Application.TodoLists.Commands.DeleteTodoList;
using CleanArchitectureSolution.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CleanArchitectureSolution.Application.IntegrationTests.TodoLists.Commands;

using static Testing;

public class DeleteTodoListTests : TestBase
{
    [Test]
    public async Task ShouldRequireValidTodoListId()
    {
        var command = new DeleteTodoListCommand { Id = 99 };
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoList()
    {
        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        await SendAsync(new DeleteTodoListCommand
        {
            Id = listId
        });

        var list = await FindAsync<TodoList>(listId);

        list.Should().BeNull();
    }
}
