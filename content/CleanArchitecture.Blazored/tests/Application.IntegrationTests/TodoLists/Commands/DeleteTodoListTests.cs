using Ardalis.GuardClauses;
using CleanArchitecture.Blazored.Application.TodoLists.Commands;
using CleanArchitecture.Blazored.Domain.Entities;
using CleanArchitecture.Blazored.WebUi.Shared.TodoLists;

namespace CleanArchitecture.Blazored.Application.IntegrationTests.TodoLists.Commands;

using static Testing;

public class DeleteTodoListTests : BaseTestFixture
{
    [Test]
    public async Task ShouldRequireValidTodoListId()
    {
        var command = new DeleteTodoListCommand(99);

        await FluentActions.Invoking(() =>
            SendAsync(command)).Should().ThrowAsync<NotFoundException>();
    }

    [Test]
    public async Task ShouldDeleteTodoList()
    {
        var listId = await SendAsync(new CreateTodoListCommand(
            new CreateTodoListRequest
            {
                Title = "New List"
            }));

        await SendAsync(new DeleteTodoListCommand(listId));

        var list = await FindAsync<TodoList>(listId);

        list.Should().BeNull();
    }
}
