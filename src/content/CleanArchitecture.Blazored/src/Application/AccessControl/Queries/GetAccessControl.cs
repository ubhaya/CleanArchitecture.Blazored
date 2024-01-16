using CleanArchitecture.Blazored.Application.Common.Services.Identity;
using CleanArchitecture.Blazored.WebUi.Shared.AccessControl;
using CleanArchitecture.Blazored.WebUi.Shared.Authorization;

namespace CleanArchitecture.Blazored.Application.AccessControl.Queries;

public sealed record GetAccessControl() : IRequest<AccessControlVm>;

public sealed class GetAccessControlQueryHandler 
    : IRequestHandler<GetAccessControl, AccessControlVm>
{
    private readonly IIdentityService _identityService;

    public GetAccessControlQueryHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<AccessControlVm> Handle(GetAccessControl request, 
        CancellationToken cancellationToken)
    {
        var permissions = PermissionsProvider.GetAll()
            .Where(permission => permission != Permissions.None)
            .ToList();

        var roles = await _identityService.GetRolesAsync(cancellationToken);
        
        var result = new AccessControlVm(roles, permissions);
        
        return result;
    }
}
