using CleanArchitecture.Maui.Application.TodoLists.Commands;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using CleanArchitecture.Maui.MobileUi.Shared.TodoLists;
using MediatR;

namespace CleanArchitecture.Maui.MobileUi.WebApi.Endpoints.TodoLists;

public sealed class PostTodoListEndpoint : IEndpointsDefinition
{
    public void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPost("api/TodoLists", PostTodoList)
            .RequireAuthorization(Permissions.Todo)
            .WithName("TodoLists_PostTodoList")
            .WithGroupName("TodoLists")
            .WithTags(["TodoLists"]);
    }

    private async Task<int> PostTodoList(CreateTodoListRequest request, ISender sender,
        CancellationToken cancellationToken)
    {
        return await sender.Send(new CreateTodoListCommand(request), cancellationToken);
    }
}