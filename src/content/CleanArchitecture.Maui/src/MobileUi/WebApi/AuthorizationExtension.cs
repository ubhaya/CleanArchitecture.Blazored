using CleanArchitecture.Maui.MobileUi.Shared.Authorization;

namespace CleanArchitecture.Maui.MobileUi.WebApi;

public static class AuthorizationExtension
{
    public static RouteHandlerBuilder RequireAuthorization(this RouteHandlerBuilder builder, Permissions permissions)
    {
        builder.RequireAuthorization(policyBuilder =>
            policyBuilder.AddRequirements(new PermissionAuthorizationRequirement(permissions)));
        return builder;
    }
}