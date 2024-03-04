# CleanArchitecture Maui

[![CleanArchitecture.MudBlazored](https://github.com/ubhaya/CleanArchitecture.Blazored/actions/workflows/CleanArchitecture.MudBlazored.yml/badge.svg)](https://github.com/ubhaya/CleanArchitecture.Blazored/actions/workflows/CleanArchitecture.MudBlazored.yml)

This is a solution template for creating a Maui solution  following the principles of Clean Architecture.

Please consider this a preview, I am still actively working on this template. If you spot a problem or would like to suggest an improvement, please let me know by creating an issue.

If you find this project useful, please give it a star. Thanks! ⭐

## Getting Started
The solution template requires the latest version of [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).

[Install the project template](../../../README.md)

### Create a new app:

```bash
dotnet new clean-architecture-maui --output CleanArchitecture
```

### Launch the app:

### Identity Server
```powershell
dotnet run --project .\CleanArchitecture\src\MobileUi\IdentityServer\
```

### Web Api
```powershell
dotnet run --project .\CleanArchitecture\src\MobileUi\WebApi\ -lp https
```

### Mobile App

Start the Mobile app in ```.\CleanArchitecture\src\MobileUi\Mobile``` using your favorite IDE.

## Database
### Configuration
The template is currently configured to use [SQL Server Express LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16) for development.

### Migrations
The template uses Entity Framework Core and migrations can be run using the EF Core CLI Tools. Install the tools using the following command:

```powershell
dotnet tool install --global dotnet-ef
```

Once installed, create a new migration with the following commands:

#### Web Api
```powershell
cd src\Infrastructure
dotnet ef migrations add "Initial" --startup-project ..\MobileUi\WebApi --context ApplicationDbContext --out-dir Data\Migrations
```
#### [Identity Server](https://duendesoftware.com/)
```powershell
cd src\Infrastructure.IdentityServer
dotnet ef migrations add "Initial" --startup-project ..\MobileUi\IdentityServer --context IdentityServerDbContext --out-dir Data\Migrations\ApplicationDb
dotnet ef migrations add "InitialIdentityServerPersistedGrantDbMigration" --startup-project ..\MobileUi\IdentityServer --context PersistedGrantDbContext --out-dir Data\Migrations\PersistedGrantDb
dotnet ef migrations add "InitialIdentityServerConfigurationDbMigration" --startup-project ..\MobileUi\IdentityServer --context ConfigurationDbContext --out-dir Data\Migrations\ConfigurationDb
```

Review the [Entity Framework Core tools reference - .NET Core CLI | Microsoft Docs](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) to learn more.

## Resources
The following resources are highly recommended:

* [Dotnet MAUI](https://dotnet.microsoft.com/en-us/apps/maui)

* [DUENDE IdentityServer](https://duendesoftware.com/)
* [Oidc Client](https://github.com/IdentityModel/IdentityModel.OidcClient)
* [Deploy Azure resources by using Bicep and GitHub Actions | Microsoft Learn](https://learn.microsoft.com/en-us/training/paths/bicep-github-actions/)

* [Automate administrative tasks by using PowerShell - Training | Microsoft Learn](https://learn.microsoft.com/en-us/training/paths/powershell/)

## License
This project is licensed with the [MIT license](https://github.com/ubhaya/CleanArchitecture.Blazored/blob/main/LICENSE).