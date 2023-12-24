using CleanArchitecture.Blazored.Application.Common.Services.DateTime;

namespace CleanArchitecture.Blazored.Infrastructure.DateTime;

public class DateTimeProvider : IDateTimeProvider
{
    public System.DateTime UtcNow => System.DateTime.UtcNow;
}
