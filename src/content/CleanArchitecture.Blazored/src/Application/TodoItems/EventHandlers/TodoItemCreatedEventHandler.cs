using Microsoft.Extensions.Logging;
using CleanArchitecture.Blazored.Domain.Events;
using Microsoft.Extensions.DependencyInjection.Common.Logging;

namespace CleanArchitecture.Blazored.Application.TodoItems.EventHandlers;

public class TodoItemCreatedEventHandler : INotificationHandler<TodoItemCreatedEvent>
{
    private readonly ILogger<TodoItemCreatedEventHandler> _logger;

    public TodoItemCreatedEventHandler(ILogger<TodoItemCreatedEventHandler> logger)
    {
        _logger = logger;
    }

    public Task Handle(TodoItemCreatedEvent notification, CancellationToken cancellationToken)
    {
        _logger.CleanArchitectureDomainEvent(notification.GetType().Name);

        return Task.CompletedTask;
    }
}
