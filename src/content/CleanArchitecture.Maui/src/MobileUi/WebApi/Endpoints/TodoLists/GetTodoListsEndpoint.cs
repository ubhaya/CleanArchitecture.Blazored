using CleanArchitecture.Maui.Application.TodoLists.Queries;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using CleanArchitecture.Maui.MobileUi.Shared.TodoLists;
using MediatR;

namespace CleanArchitecture.Maui.MobileUi.WebApi.Endpoints.TodoLists;

public sealed class GetTodoListsEndpoint : IEndpointsDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("api/TodoLists/", GetTodoLists)
            .RequireAuthorization(Permissions.Todo)
            .WithName("Get Todo Lists")
            .WithOpenApi();
    }

    private async Task<TodosVm> GetTodoLists(ISender sender, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetTodoListsQuery(),cancellationToken);
    }
}