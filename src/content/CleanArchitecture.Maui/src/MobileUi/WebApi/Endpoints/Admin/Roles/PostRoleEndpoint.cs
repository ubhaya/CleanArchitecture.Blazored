using CleanArchitecture.Maui.Application.Roles.Commands;
using CleanArchitecture.Maui.MobileUi.Shared.AccessControl;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Maui.MobileUi.WebApi.Endpoints.Admin.Roles;

public sealed class PostRoleEndpoint : IEndpointsDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPost("api/Admin/Roles", PostRole)
            .RequireAuthorization(Permissions.ManageRoles)
            .Produces(StatusCodes.Status204NoContent)
            .WithName("Post Roles")
            .WithOpenApi();
    }

    private static async Task<IResult> PostRole([FromBody] RoleDto newRole, ISender sender,
        CancellationToken cancellationToken)
    {
        await sender.Send(new CreateRoleCommand(newRole), cancellationToken);
        return Results.NoContent();
    }
}