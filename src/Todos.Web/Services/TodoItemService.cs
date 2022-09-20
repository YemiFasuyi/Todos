using System.Net.Http.Json;
using Todos.Orchestrator.TodoItems.Commands;
using Todos.Orchestrator.TodoItems.Models;

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

    public async Task<int> CreateTodoItemAsync(CreateTodoItemCommand command)
    {
        var response = await _client.PostAsJsonAsync("/todoItem", command);
        if (!response.IsSuccessStatusCode)
            return 0;

        _ = int.TryParse(await response.Content.ReadAsStringAsync(), out int result);
        return result;
    }

    public async Task<bool> UpdateTodoItemStatusAsync(UpdateTodoItemStatusCommand command)
    {
        var response = await _client.PostAsJsonAsync("/todoItem/status", command);
        if (!response.IsSuccessStatusCode)
            return false;

        _ = bool.TryParse(await response.Content.ReadAsStringAsync(), out bool result);
        return result;
    }
}