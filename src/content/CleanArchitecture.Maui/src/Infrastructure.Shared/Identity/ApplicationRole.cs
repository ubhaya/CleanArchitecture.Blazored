using Microsoft.AspNetCore.Identity;
using CleanArchitecture.Maui.MobileUi.Shared.Authorization;

namespace CleanArchitecture.Maui.Infrastructure.Identity;

public class ApplicationRole : IdentityRole
{
    public Permissions Permissions { get; set; }
}
