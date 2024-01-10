using Microsoft.Extensions.Logging;
using CleanArchitecture.Blazored.Domain.Events;
using Microsoft.Extensions.DependencyInjection.Common.Logging;

namespace CleanArchitecture.Blazored.Application.TodoItems.EventHandlers;

public class TodoItemCompletedEventHandler : INotificationHandler<TodoItemCompletedEvent>
{
    private readonly ILogger<TodoItemCompletedEventHandler> _logger;

    public TodoItemCompletedEventHandler(ILogger<TodoItemCompletedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoItemCompletedEvent notification, CancellationToken cancellationToken)
    {
        _logger.CleanArchitectureDomainEvent(notification.GetType().Name);

        return Task.CompletedTask;
    }
}
