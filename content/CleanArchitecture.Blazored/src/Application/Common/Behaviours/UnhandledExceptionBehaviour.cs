using Microsoft.Extensions.DependencyInjection.Common.Logging;
using Microsoft.Extensions.Logging;

namespace CleanArchitecture.Blazored.Application.Common.Behaviours;

public sealed class UnhandledExceptionBehaviour<TRequest, TResponse>
    : IPipelineBehavior<TRequest,TResponse>
    where TRequest : IRequest<TResponse>
{
    private readonly ILogger<TRequest> _logger;

    public UnhandledExceptionBehaviour(ILogger<TRequest> logger)
    {
        _logger = logger;
    }

    public async Task<TResponse> Handle(
        TRequest request, 
        RequestHandlerDelegate<TResponse> next, 
        CancellationToken cancellationToken)
    {
        try
        {
            return await next();
        }
        catch (Exception ex)
        {
            var requestName = typeof(TRequest).Name;
            
            _logger.CleanArchitectureUnhandledException(ex, requestName, request.ToString());

            throw;
        }
    }
}
