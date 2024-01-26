using CleanArchitecture.Maui.Application.Users.Queries;
using CleanArchitecture.Maui.MobileUi.Shared.AccessControl;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using MediatR;

namespace CleanArchitecture.Maui.MobileUi.WebApi.Endpoints.Admin.Users;

public sealed class GetUserEndpoint : IEndpointsDefinition
{
    public void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("api/Admin/Users/{id}", GetUser)
            .RequireAuthorization(Permissions.ViewUsers)
            .WithName("Users_GetUser")
            .WithGroupName("Users")
            .WithTags(["Users"]);
    }

    private static async Task<UserDetailsVm> GetUser(string id, ISender sender, CancellationToken cancellationToken)
    {
        return await sender.Send(new GetUserQuery(id), cancellationToken);
    }
}