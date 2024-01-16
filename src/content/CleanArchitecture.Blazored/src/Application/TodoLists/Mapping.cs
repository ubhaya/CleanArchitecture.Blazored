using CleanArchitecture.Blazored.Domain.Entities;
using CleanArchitecture.Blazored.WebUi.Shared.TodoLists;
using Riok.Mapperly.Abstractions;

namespace CleanArchitecture.Blazored.Application.TodoLists;

[Mapper]
public static partial class Mapping
{
    public static partial IQueryable<TodoListDto> ProjectToDto(this IQueryable<TodoList> s);
}
