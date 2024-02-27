using CleanArchitecture.Maui.MobileUi.Shared.TodoLists;
using CommunityToolkit.Mvvm.ComponentModel;

namespace CleanArchitecture.Maui.MobileUi.Mobile.Models.Todo;

public sealed partial class TodoItemsModel : ObservableObject
{
    [ObservableProperty] private bool _done;
    [ObservableProperty] private string _title = string.Empty;

    public int Id { get; set; }

    public int ListId { get; set; }

    public int Priority { get; set; }

    public string Note { get; set; } = string.Empty;

    public static TodoItemsModel From(TodoItemDto source)
    {
        return new TodoItemsModel
        {
            Id = source.Id,
            ListId = source.ListId,
            Title = source.Title,
            Done = source.Done,
            Priority = source.Priority,
            Note = source.Note,
        };
    }
}