using CleanArchitecture.Maui.Application.TodoItems.Commands;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using CleanArchitecture.Maui.MobileUi.Shared.TodoItems;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Maui.MobileUi.WebApi.Endpoints.TodoItems;

public sealed class PutTodoItemEndpoint : IEndpointsDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPut("api/TodoItems/{id:int}", PutTodoItem)
            .RequireAuthorization(Permissions.Todo)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status400BadRequest)
            .WithName("Put Todo Item")
            .WithOpenApi();
    }

    private async Task<IResult> PutTodoItem([FromRoute] int id, [FromBody] UpdateTodoItemRequest request, ISender sender,
        CancellationToken cancellationToken)
    {
        if (id != request.Id) return Results.BadRequest();
        await sender.Send(new UpdateTodoItemCommand(request), cancellationToken);
        return Results.NoContent();
    }
}