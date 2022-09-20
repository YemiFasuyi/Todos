using System.Net.Http.Json;
using Todos.Orchestrator.Todos.Models;

namespace Todos.Web.Services;

public class TodoItemService
{
    private readonly HttpClient _client;

    public TodoItemService(HttpClient client)
    {
        _client = client;
    }

    public async Task<List<TodoItemViewModel>?> GetTodoItemsAsync()
    {
        try
        {
            return await _client.GetFromJsonAsync<List<TodoItemViewModel>?>("todoItems");
        }
        catch (Exception)
        {
            return null;
        }
    }
}