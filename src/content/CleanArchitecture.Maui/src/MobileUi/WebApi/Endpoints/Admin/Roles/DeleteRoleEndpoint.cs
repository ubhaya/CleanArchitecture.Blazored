using CleanArchitecture.Maui.Application.Roles.Commands;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using MediatR;

namespace CleanArchitecture.Maui.MobileUi.WebApi.Endpoints.Admin.Roles;

public sealed class DeleteRoleEndpoint : IEndpointsDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapDelete("api/Admin/{id}", DeleteRole)
            .RequireAuthorization(Permissions.ManageRoles)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("Delete Role")
            .WithOpenApi();
    }

    private static async Task<IResult> DeleteRole(string id, ISender sender, CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteRoleCommand(id), cancellationToken);
        return Results.NoContent();
    }
}