namespace CleanArchitecture.Blazored.Application.Common.Services.DateTime;

public interface IDateTimeProvider
{
    System.DateTime UtcNow { get; }
}
