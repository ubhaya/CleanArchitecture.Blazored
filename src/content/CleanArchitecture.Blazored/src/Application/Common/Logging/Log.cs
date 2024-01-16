using Microsoft.Extensions.Logging;

namespace Microsoft.Extensions.DependencyInjection.Common.Logging;

public static partial class LogExtension
{
    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Information,
        Message = "CleanArchitecture Domain Event: {DomainEvent}")]
    public static partial void CleanArchitectureDomainEvent(
        this ILogger logger, string domainEvent);

    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Information,
        Message = "CleanArchitecture Request: {Name} {@UserId} {@UserName} {@Request}")]
    public static partial void CleanArchitectureRequest
        (this ILogger logger, string name, string @userId, string @userName, string? @request);

    [LoggerMessage(
        EventId = 0,
        Level = LogLevel.Error,
        Message = "CleanArchitecture Request: Unhandled Exception for Request {Name} {@Request}")]
    public static partial void CleanArchitectureUnhandledException(
        this ILogger logger, Exception ex, string name, string? request);
}