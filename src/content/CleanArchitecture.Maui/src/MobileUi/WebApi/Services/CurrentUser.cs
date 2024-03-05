using System.Security.Claims;
using CleanArchitecture.Maui.Application.Common.Services.Identity;

namespace CleanArchitecture.Maui.MobileUi.WebApi.Services;

public class CurrentUser : ICurrentUser
{
    private readonly IHttpContextAccessor _httpContextAccessor;

    public CurrentUser(IHttpContextAccessor httpContextAccessor)
    {
        _httpContextAccessor = httpContextAccessor;
    }

    public string? UserId => _httpContextAccessor.HttpContext?
        .User?
        .FindFirstValue(ClaimTypes.NameIdentifier);
}
