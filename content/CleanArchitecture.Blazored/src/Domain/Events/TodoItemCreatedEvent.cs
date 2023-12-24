using CleanArchitecture.Blazored.Domain.Common;
using CleanArchitecture.Blazored.Domain.Entities;

namespace CleanArchitecture.Blazored.Domain.Events;

public sealed class TodoItemCreatedEvent : BaseEvent
{
    public TodoItem Item { get; }

    public TodoItemCreatedEvent(TodoItem item)
    {
        Item = item;
    }
}
