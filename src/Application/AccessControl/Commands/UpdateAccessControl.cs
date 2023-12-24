using CleanArchitecture.Blazored.Application.Common.Services.Identity;
using CleanArchitecture.Blazored.WebUi.Shared.Authorization;

namespace CleanArchitecture.Blazored.Application.AccessControl.Commands;

public sealed record UpdateAccessControlCommand(string RoleId, Permissions Permissions) : IRequest<Unit>;

public sealed class UpdateAccessControlCommandHandler 
    : IRequestHandler<UpdateAccessControlCommand, Unit>
{
    private readonly IIdentityService _identityService;

    public UpdateAccessControlCommandHandler(IIdentityService identityService)
    {
        _identityService = identityService;
    }

    public async Task<Unit> Handle(UpdateAccessControlCommand request, 
        CancellationToken cancellationToken)
    {
        await _identityService.UpdateRolePermissionsAsync(request.RoleId, request.Permissions);
        return new Unit();
    }
}
