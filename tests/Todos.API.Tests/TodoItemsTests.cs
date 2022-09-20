using System.Net.Http.Json;
using Microsoft.Extensions.DependencyInjection;
using Todos.Domain.TodoItems;
using Todos.Orchestrator.Todos.Models;
using Todos.Persistance;

namespace Todos.API.Tests;

public class TodoItemsTests
{
    [Fact]
    public async void TodoItemsTest()
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
}