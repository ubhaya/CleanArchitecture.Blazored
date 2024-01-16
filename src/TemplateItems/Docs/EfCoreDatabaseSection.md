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