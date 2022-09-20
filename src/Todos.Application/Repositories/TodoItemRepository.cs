using Microsoft.EntityFrameworkCore;
using Todos.Application.Interfaces;
using Todos.Domain;
using Todos.Persistance;

namespace Todos.Application.Services;

public class TodoItemRepository : ITodoItemRepository
{
    private readonly ITodoDbContext _db;

    public TodoItemRepository(ITodoDbContext db)
    {
        _db = db;
    }

    public async Task<int> CreateTodoItemAsync(string description, TodoItemStatusEnum status, CancellationToken cancellationToken = new CancellationToken())
    {
        var todoItem = new TodoItem
        {
            Description = description,
            Status = status,
            Created = DateTime.UtcNow,
            Updated = DateTime.UtcNow,
        };

        await _db.TodoItems.AddAsync(todoItem, cancellationToken);
        await _db.SaveChangesAsync(cancellationToken);

        return todoItem.Id;
    }

    public async Task<List<TodoItem>> GetTodoItemsAsync(CancellationToken cancellationToken = new CancellationToken())
    {
        return await _db.TodoItems
            .ToListAsync(cancellationToken);
    }

    public async Task<TodoItem?> GetTodoItemAsync(int id, CancellationToken cancellationToken = new CancellationToken())
    {
        return await _db.TodoItems
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task UpdateTodoItemAsync(int id, string description, TodoItemStatusEnum status, CancellationToken cancellationToken = new CancellationToken())
    {
        var todoItem = await _db.TodoItems
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (todoItem == null)
            throw new Exception($"Unable to retrieve todo item for id: {id}");

        todoItem.Description = description;
        todoItem.Status = status;
        todoItem.Updated = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);
    }

    public async Task DeleteTodoItemAsync(int id, CancellationToken cancellationToken = new CancellationToken())
    {
        var todoItem = await _db.TodoItems
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);

        if (todoItem == null)
            throw new Exception($"Unable to retrieve todo item for id: {id}");

        todoItem.Deleted = DateTime.UtcNow;

        await _db.SaveChangesAsync(cancellationToken);
    }
}