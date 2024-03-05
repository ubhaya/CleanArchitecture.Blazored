using CleanArchitecture.Maui.Application.Roles.Commands;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using MediatR;

namespace CleanArchitecture.Maui.MobileUi.WebApi.Endpoints.Admin.Roles;

public sealed class DeleteRoleEndpoint : IEndpointsDefinition
{
    public void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapDelete("api/Admin/Roles/{id}", DeleteRole)
            .RequireAuthorization(Permissions.ManageRoles)
            .Produces(StatusCodes.Status204NoContent)
            .Produces(StatusCodes.Status404NotFound)
            .WithName("Roles_DeleteRole")
            .WithGroupName("Roles")
            .WithTags(["Roles"]);
    }

    private static async Task<IResult> DeleteRole(string id, ISender sender, CancellationToken cancellationToken)
    {
        await sender.Send(new DeleteRoleCommand(id), cancellationToken);
        return Results.NoContent();
    }
}