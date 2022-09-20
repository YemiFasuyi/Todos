using System.Net;
using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Todos.Domain.TodoItems;
using Todos.Orchestrator.TodoItems.Commands;
using Todos.Orchestrator.TodoItems.Models;
using Todos.Persistance;

namespace Todos.API.Tests;

public class TodoItemsEndpointTests
{
    [Fact]
    public async void GetTodoItemsTest()
    {
        await using var app = new TestApplicationFactory();

        using var scope = app.Services.CreateScope();
        var provider = scope.ServiceProvider;
        using var todoDbContext = provider.GetRequiredService<TodoDbContext>();
        await todoDbContext.TodoItems.AddAsync(new() { Id = 1, Description = "Test", Status = TodoItemStatusEnum.Pending, Created = DateTime.UtcNow, Updated = DateTime.UtcNow });
        await todoDbContext.SaveChangesAsync();

        var client = app.CreateClient();
        var todoItems = await client.GetFromJsonAsync<List<TodoItemViewModel>>("/todoItems");

        Assert.NotNull(todoItems);
        Assert.True(todoItems.Count == 1);
    }

    [Fact]
    public async void CreateTodoItemTest()
    {
        await using var app = new TestApplicationFactory();

        var client = app.CreateClient();
        var result = await client.PostAsJsonAsync("/todoItem", new CreateTodoItemCommand
        {
            Description = "TodoItem",
        });

        _ = int.TryParse(await result.Content.ReadAsStringAsync(), out int id);

        Assert.Equal(HttpStatusCode.OK, result.StatusCode);
        Assert.True(id > 0);
    }

    [Fact]
    public async void UpdateTodoItemStatusTest()
    {
        await using var app = new TestApplicationFactory();

        var todoItem = new CreateTodoItemCommand
        {
            Description = "TodoItem",
        };

        var client = app.CreateClient();
        var createResult = await client.PostAsJsonAsync("/todoItem", todoItem);

        _ = int.TryParse(await createResult.Content.ReadAsStringAsync(), out int id);


        var updateResult = await client.PostAsJsonAsync("/todoItem/status", new UpdateTodoItemStatusCommand
        {
            Id = id,
            Status = TodoItemStatus.Completed,
        });

        _ = bool.TryParse(await updateResult.Content.ReadAsStringAsync(), out bool success);

        Assert.Equal(HttpStatusCode.OK, updateResult.StatusCode);
        Assert.True(success);
    }
}