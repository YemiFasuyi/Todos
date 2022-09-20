using Microsoft.EntityFrameworkCore;
using Todos.Domain;

namespace Todos.Persistance;

public interface ITodoDbContext
{
    DbSet<TodoItem> TodoItems { get; }

    Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
}