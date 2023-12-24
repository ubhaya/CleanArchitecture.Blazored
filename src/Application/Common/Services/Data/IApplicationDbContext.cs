using CleanArchitecture.Blazored.Domain.Entities;

namespace CleanArchitecture.Blazored.Application.Common.Services.Data;

public interface IApplicationDbContext
{
    DbSet<TEntity> Set<TEntity>() where TEntity : class;
    
    DbSet<TodoList> TodoLists { get; }
    
    DbSet<TodoItem> TodoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}
