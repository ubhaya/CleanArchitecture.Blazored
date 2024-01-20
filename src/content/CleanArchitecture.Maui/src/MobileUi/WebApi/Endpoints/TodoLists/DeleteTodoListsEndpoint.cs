using CleanArchitecture.Maui.Application.TodoItems.Commands;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using MediatR;

namespace CleanArchitecture.Maui.MobileUi.WebApi.Endpoints.TodoLists;

public sealed class DeleteTodoListsEndpoint : IEndpointsDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapDelete("api/TodoLists/{id:int}", DeleteTodoList)
            .RequireAuthorization(Permissions.Todo)
            .Produces(StatusCodes.Status204NoContent)
            .WithName("Delete Todo Lists")
            .WithOpenApi();
    }

    private async Task<IResult> DeleteTodoList(int id, ISender sender, CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteTodoItemCommand(id), cancellationToken);

        return Results.NoContent();
    }
}