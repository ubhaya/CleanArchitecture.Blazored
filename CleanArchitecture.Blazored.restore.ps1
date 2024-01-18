dotnet pack ./src/CleanArchitecture.Blazored.MsBuild/ -o ./artifacts
dotnet nuget update source CleanArchitecture.Blazored.Dev --configfile ./nuget.config
dotnet restore ./src/CleanArchitecture.Blazored.csproj --configfile ./nuget.config
dotnet build ./src/CleanArchitecture.Blazored.csproj --no-restore -c Release