using Microsoft.EntityFrameworkCore;
using Todos.Domain.TodoItems;

namespace Todos.Persistance;

public class TodoDbContext : DbContext, ITodoDbContext
{
    public TodoDbContext(DbContextOptions<TodoDbContext> options) : base(options)
    {
        Database.EnsureCreated();
    }

    public DbSet<TodoItem> TodoItems => Set<TodoItem>();

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<TodoItem>().HasQueryFilter(x => x.Deleted == null);
    }

    public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
    {
        return await base.SaveChangesAsync(cancellationToken);
    }
}