﻿using Ardalis.GuardClauses;
using CleanArchitecture.Blazored.Application.Common.Exceptions;
using CleanArchitecture.Blazored.Application.TodoLists.Commands;
using CleanArchitecture.Blazored.Domain.Entities;
using CleanArchitecture.Blazored.WebUi.Shared.TodoLists;

namespace CleanArchitecture.Blazored.Application.IntegrationTests.TodoLists.Commands;

using static Testing;

public class UpdateTodoListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoListId()
    {
        var command = new UpdateTodoListCommand(
            new UpdateTodoListRequest
            {
                Id = 99,
                Title = "New Title"
            });

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldRequireUniqueTitle()
    {
        var listId = await SendAsync(new CreateTodoListCommand(
            new CreateTodoListRequest { Title = "New List" }));

        await SendAsync(new CreateTodoListCommand(
            new CreateTodoListRequest { Title = "Other List" }));

        var command = new UpdateTodoListCommand(
            new UpdateTodoListRequest
            {
                Id = listId,
                Title = "Other List"
            });

        (await FluentActions.Invoking(() =>
                    SendAsync(command))
                .Should().ThrowAsync<ValidationException>().Where(ex => ex.Errors.ContainsKey("List.Title")))
                .And.Errors["List.Title"].Should().Contain("'Title' must be unique.");
    }

    [Test]
    public async Task ShouldUpdateTodoList()
    {
        var userId = await RunAsDefaultUserAsync();

        var listId = await SendAsync(new CreateTodoListCommand(
            new CreateTodoListRequest { Title = "New List" }));

        var command = new UpdateTodoListCommand(
            new UpdateTodoListRequest
            {
                Id = listId,
                Title = "Updated List Title"
            });

        await SendAsync(command);

        var list = await FindAsync<TodoList>(listId);

        list.Should().NotBeNull();
        list!.Title.Should().Be(command.List.Title);
        list.LastModifiedBy.Should().NotBeNull();
        list.LastModifiedBy.Should().Be(userId);
        list.LastModifiedUtc.Should().BeCloseTo(DateTime.UtcNow, TimeSpan.FromMilliseconds(10000));
    }
}
