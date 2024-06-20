using CleanArchitecture.Maui.Application.AccessControl.Queries;
using CleanArchitecture.Maui.MobileUi.Shared.AccessControl;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;
using MediatR;

namespace CleanArchitecture.Maui.MobileUi.WebApi.Endpoints.Admin.AccessControl;

public sealed class GetConfigurationEndpoint : IEndpointsDefinition
{
    public void DefineEndpoints(IEndpointRouteBuilder app)
    {
        app.MapGet("api/Admin/AccessControl",
                GetConfiguration)
            .RequireAuthorization(Permissions.ViewAccessControl)
            .WithName("AccessControl_GetConfiguration")
            .WithGroupName("AccessControl")
            .WithTags(["AccessControl"]);
    }

    private static async Task<AccessControlVm> GetConfiguration(IMediator mediator, CancellationToken cancellationToken)
    {
        return await mediator.Send(new GetAccessControl(), cancellationToken);
    }
}