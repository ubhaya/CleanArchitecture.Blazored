using CleanArchitecture.Maui.Application.AccessControl.Commands;
using CleanArchitecture.Maui.MobileUi.Shared.AccessControl;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace CleanArchitecture.Maui.MobileUi.WebApi.Endpoints.Admin.AccessControl;

public sealed class UpdateConfigurationEndpoint : IEndpointsDefinition
{
    public void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapPut("api/Admin/AccessControl", PutUpdateConfiguration)
            .Produces(StatusCodes.Status204NoContent)
            .RequireAuthorization(Permissions.ConfigureAccessControl)
            .WithName("AccessControl_UpdateConfiguration")
            .WithGroupName("Admin")
            .WithTags(["AccessControl"]);
    }

    private static async Task<IResult> PutUpdateConfiguration(ISender mediator, [FromBody] RoleDto updatedRole,
        CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateAccessControlCommand(updatedRole.Id, updatedRole.Permissions), cancellationToken);
        return Results.NoContent();
    }
}