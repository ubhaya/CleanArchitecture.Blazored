using CleanArchitecture.Maui.Application.AccessControl.Commands;
using CleanArchitecture.Maui.MobileUi.Shared.AccessControl;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using MediatR;
using Microsoft.AspNetCore.Http.HttpResults;

namespace CleanArchitecture.Maui.MobileUi.WebApi.Endpoints.Admin.AccessControl;

public sealed class UpdateConfigurationEndpoint : IEndpointsDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapPut("api/Admin/UpdateConfiguration", PutUpdateConfiguration)
            .Produces(StatusCodes.Status204NoContent)
            .RequireAuthorization(Permissions.ConfigureAccessControl);
    }

    private static async Task<IResult> PutUpdateConfiguration(ISender mediator, RoleDto updatedRole,
        CancellationToken cancellationToken)
    {
        await mediator.Send(new UpdateAccessControlCommand(updatedRole.Id, updatedRole.Permissions), cancellationToken);
        return Results.NoContent();
    }
}