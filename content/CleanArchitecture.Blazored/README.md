# CleanArchitecture Bootstrap Blazored Web App

> **Note:-** This project is a port from [RapidBlazor](https://github.com/jasontaylordev/RapidBlazor) Template

This is a solution template for creating a Blazor Web App with client side rendering and following the principles of Clean Architecture.

Please consider this a preview, I am still actively working on this template. If you spot a problem or would like to suggest an improvement, please let me know by creating an issue.

If you find this project useful, please give it a star. Thanks! ⭐

## Getting Started
The solution template requires the latest version of [.NET 8 SDK](https://dotnet.microsoft.com/en-us/download/dotnet/8.0).

Install the project template:

```bash
dotnet new install CleanArchitecture.Blazored
```

Create a new app:

```bash
dotnet new clean-architecture-blazored --output CleanArchitecture
```

Launch the app:
```bash
cd CleanArchitecture\src\WebUi\WebUi
dotnet run
```

## Database
### Configuration
The template is currently configured to use [SQL Server Express LocalDB](https://learn.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-express-localdb?view=sql-server-ver16) for development.

### Migrations
The template uses Entity Framework Core and migrations can be run using the EF Core CLI Tools. Install the tools using the following command:

```bash
dotnet tool install --global dotnet-ef
```

Once installed, create a new migration with the following commands:

```bash
cd src\Infrastructure
dotnet ef migrations add "Initial" --startup-project ..\WebUi\WebUi --context ApplicationDbContext --out-dir Data\Migrations
```

Review the [Entity Framework Core tools reference - .NET Core CLI | Microsoft Docs](https://learn.microsoft.com/en-us/ef/core/cli/dotnet) to learn more.

## Resources
The following resources are highly recommended:

* [Blazor Workshop | .NET Presentations: Events in a Box!](https://github.com/dotnet-presentations/blazor-workshop)

* [Deploy Azure resources by using Bicep and GitHub Actions | Microsoft Learn](https://learn.microsoft.com/en-us/training/paths/bicep-github-actions/)

* [Automate administrative tasks by using PowerShell - Training | Microsoft Learn](https://learn.microsoft.com/en-us/training/paths/powershell/)

## License
This project is licensed with the [MIT license](https://github.com/ubhaya/CleanArchitecture.Blazored/blob/main/LICENSE).