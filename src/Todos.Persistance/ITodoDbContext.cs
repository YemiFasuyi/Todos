using Microsoft.EntityFrameworkCore;
using Todos.Domain.TodoItems;

namespace Todos.Persistance;

public interface ITodoDbContext
{
    DbSet<TodoItem> TodoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}