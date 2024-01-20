using CleanArchitecture.Maui.Application.Roles.Queries;
using CleanArchitecture.Maui.MobileUi.Shared.AccessControl;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using MediatR;

namespace CleanArchitecture.Maui.MobileUi.WebApi.Endpoints.Admin.Roles;

public sealed class GetRolesEndpoint : IEndpointsDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("api/Admin/Roles", GetRoles)
            .RequireAuthorization(Permissions.ViewRoles)
            .WithName("Get Roles")
            .WithOpenApi();
    }

    private static async Task<RolesVm> GetRoles(ISender sender, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetRolesQuery(), cancellationToken);
    }
}