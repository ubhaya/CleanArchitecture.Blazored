using CleanArchitecture.Maui.Application.TodoItems.Commands;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using CleanArchitecture.Maui.MobileUi.Shared.TodoItems;
using MediatR;

namespace CleanArchitecture.Maui.MobileUi.WebApi.Endpoints.TodoItems;

public sealed class PostTodoItemEndpoint : IEndpointsDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("api/TodoItems", PostTodoItem)
            .RequireAuthorization(Permissions.Todo)
            .WithName("Post Todo Item")
            .WithOpenApi();
    }

    private async Task<int> PostTodoItem(CreateTodoItemRequest request, ISender sender, CancellationToken cancellationToken)
    {
        return await sender.Send(new CreateTodoItemCommand(request), cancellationToken);
    }
}