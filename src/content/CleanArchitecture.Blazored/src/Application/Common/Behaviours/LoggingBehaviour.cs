using MediatR.Pipeline;
using Microsoft.Extensions.Logging;
using CleanArchitecture.Blazored.Application.Common.Services.Identity;
using Microsoft.Extensions.DependencyInjection.Common.Logging;

namespace CleanArchitecture.Blazored.Application.Common.Behaviours;

public sealed class LoggingBehaviour<TRequest>
    : IRequestPreProcessor<TRequest> where TRequest : notnull
{
    private readonly ILogger _logger;
    private readonly ICurrentUser _currentUser;
    private readonly IIdentityService _identityService;

    public LoggingBehaviour(
        ILogger logger, 
        ICurrentUser currentUser, 
        IIdentityService identityService)
    {
        _logger = logger;
        _currentUser = currentUser;
        _identityService = identityService;
    }

    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var requestName = typeof(TRequest).Name;
        var userId = _currentUser.UserId ?? string.Empty;
        var userName = string.Empty;

        if (!string.IsNullOrEmpty(userId))
        {
            userName = await _identityService.GetUserNameAsync(userId);
        }
        
        _logger.CleanArchitectureRequest(requestName, userId, userName, request.ToString());
    }
}
