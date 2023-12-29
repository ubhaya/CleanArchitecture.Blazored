using CleanArchitecture.Blazored.Domain.Common;
using CleanArchitecture.Blazored.Domain.Entities;

namespace CleanArchitecture.Blazored.Domain.Events;

public sealed class TodoItemCompletedEvent : BaseEvent
{
    public TodoItem Item { get; }

    public TodoItemCompletedEvent(TodoItem item)
    {
        Item = item;
    }
}
