using CleanArchitecture.Blazored.WebUi.Shared.Common;

namespace CleanArchitecture.Blazored.WebUi.Shared.TodoLists;

public class TodosVm
{
    public List<LookupDto> PriorityLevels { get; set; } = new();

    public List<TodoListDto> Lists { get; set; } = new();
}
