using CleanArchitecture.Blazored.Domain.Common;
using CleanArchitecture.Blazored.Domain.Enums;

namespace CleanArchitecture.Blazored.Domain.Entities;

public sealed class TodoItem : BaseAuditableEntity
{
    public int ListId { get; set; }

    public string Title { get; set; } = string.Empty;

    public string Note { get; set; } = string.Empty;

    public bool Done { get; set; }

    public DateTime? Reminder { get; set; }

    public PriorityLevel Priority { get; set; }

    public TodoList List { get; set; } = null!;
}
