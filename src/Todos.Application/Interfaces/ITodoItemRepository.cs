using Todos.Domain;

namespace Todos.Application.Interfaces
{
    public interface ITodoItemRepository
    {
        Task<int> CreateTodoItemAsync(string description, TodoItemStatusEnum status, CancellationToken cancellationToken = default);
        Task DeleteTodoItemAsync(int id, CancellationToken cancellationToken = default);
        Task<TodoItem?> GetTodoItemAsync(int id, CancellationToken cancellationToken = default);
        Task<List<TodoItem>> GetTodoItemsAsync(CancellationToken cancellationToken = default);
        Task UpdateTodoItemAsync(int id, string description, TodoItemStatusEnum status, CancellationToken cancellationToken = default);
    }
}