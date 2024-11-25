﻿using MartinTranslate.Application.TodoLists.Commands.CreateTodoList;
using MartinTranslate.Application.TodoLists.Commands.DeleteTodoList;
using MartinTranslate.Domain.Entities;

using static MartinTranslate.Application.FunctionalTests.Testing;

namespace MartinTranslate.Application.FunctionalTests.TodoLists.Commands;
public class DeleteTodoListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoListId()
    {
        var command = new DeleteTodoListCommand(99);
        await FluentActions.Invoking(() => SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoList()
    {
        var listId = await SendAsync(new CreateTodoListCommand
        {
            Title = "New List"
        });

        await SendAsync(new DeleteTodoListCommand(listId));

        var list = await FindAsync<TodoList>(listId);

        list.Should().BeNull();
    }
}
