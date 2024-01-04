FROM mcr.microsoft.com/dotnet/aspnet:8.0 AS base
USER $APP_UID
WORKDIR /app
EXPOSE 8080

USER root

RUN apt-get update && apt-get install -y curl

FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
ARG BUILD_CONFIGURATION=Release
WORKDIR /src
COPY ["./src/", "."]
RUN dotnet restore ./WebUi/WebUi/WebUi.csproj
WORKDIR "/src/WebUi/WebUi"
RUN dotnet build "WebUi.csproj" -c $BUILD_CONFIGURATION -o /app/build

FROM build AS publish
ARG BUILD_CONFIGURATION=Release
RUN dotnet publish "WebUi.csproj" -c $BUILD_CONFIGURATION -o /app/publish /p:UseAppHost=false

FROM base AS final
WORKDIR /app
COPY --from=publish /app/publish .

HEALTHCHECK --interval=30s --timeout=3s \
    CMD curl --fail http://localhost:8000/health || exit 1

ENTRYPOINT ["dotnet", "CleanArchitecture.Blazored.WebUi.dll"]
