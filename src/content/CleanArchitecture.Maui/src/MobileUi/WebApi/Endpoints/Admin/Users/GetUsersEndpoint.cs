using CleanArchitecture.Maui.Application.Users.Queries;
using CleanArchitecture.Maui.MobileUi.Shared.AccessControl;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using MediatR;

namespace CleanArchitecture.Maui.MobileUi.WebApi.Endpoints.Admin.Users;

public sealed class GetUsersEndpoint : IEndpointsDefinition
{
    public void DefineEndpoints(WebApplication app)
    {
        app.MapGet("api/Admin/Users", GetUser)
            .RequireAuthorization(Permissions.ViewUsers | Permissions.ManageUsers);
    }

    private async Task<UsersVm> GetUser(ISender sender,CancellationToken cancellationToken)
    {
        return await sender.Send(new GetUsersQuery(), cancellationToken);
    }
}