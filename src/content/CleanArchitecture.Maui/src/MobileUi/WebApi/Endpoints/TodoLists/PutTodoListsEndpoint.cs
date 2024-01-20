using CleanArchitecture.Maui.Application.TodoLists.Commands;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using CleanArchitecture.Maui.MobileUi.Shared.TodoLists;
using MediatR;

namespace CleanArchitecture.Maui.MobileUi.WebApi.Endpoints.TodoLists;

public sealed class PutTodoListsEndpoint : IEndpointsDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPut("api/TodoLists/{id}", PutTodoList)
            .RequireAuthorization(Permissions.Todo)
            .Produces(StatusCodes.Status400BadRequest)
            .Produces(StatusCodes.Status204NoContent)
            .WithName("Put Todo List")
            .WithOpenApi();
    }

    private async Task<IResult> PutTodoList(int id, UpdateTodoListRequest request, ISender sender,
        CancellationToken cancellationToken)
    {
        if (id != request.Id) return Results.BadRequest();

        await sender.Send(new UpdateTodoListCommand(request), cancellationToken);

        return Results.NoContent();
    }
}