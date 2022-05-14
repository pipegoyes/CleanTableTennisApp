﻿using CleanArchitectureSolution.Application.Common.Exceptions;
using CleanArchitectureSolution.Application.TodoItems.Commands.CreateTodoItem;
using CleanArchitectureSolution.Application.TodoItems.Commands.DeleteTodoItem;
using CleanArchitectureSolution.Application.TodoLists.Commands.CreateTodoList;
using CleanArchitectureSolution.Domain.Entities;
using FluentAssertions;
using NUnit.Framework;

namespace CleanArchitectureSolution.Application.IntegrationTests.TodoItems.Commands;

using static Testing;

public class DeleteTodoItemTests : TestBase
{
    [Test]
    public async Task ShouldRequireValidTodoItemId()
    {
        var command = new DeleteTodoItemCommand { Id = 99 };

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoItem()
    {
        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        var itemId = await SendAsync(new CreateTodoItemCommand
        {
            ListId = listId,
            Title = "New Item"
        });

        await SendAsync(new DeleteTodoItemCommand
        {
            Id = itemId
        });

        var item = await FindAsync<TodoItem>(itemId);

        item.Should().BeNull();
    }
}
