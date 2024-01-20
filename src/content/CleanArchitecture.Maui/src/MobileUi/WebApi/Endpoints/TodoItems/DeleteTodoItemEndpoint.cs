using CleanArchitecture.Maui.Application.TodoItems.Commands;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Maui.MobileUi.WebApi.Endpoints.TodoItems;

public sealed class DeleteTodoItemEndpoint : IEndpointsDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapDelete("api/TodoItems/{id:int}", DeleteTodoItem)
            .RequireAuthorization(Permissions.Todo)
            .Produces(StatusCodes.Status204NoContent)
            .WithName("Delete Todo Item")
            .WithOpenApi();
    }

    private static async Task<IResult> DeleteTodoItem([FromRoute] int id, ISender sender, CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteTodoItemCommand(id), cancellationToken);
        return Results.NoContent();
    }
}