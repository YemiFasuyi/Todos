using Microsoft.AspNetCore.Components;
using Todos.Orchestrator.Todos.Models;
using Todos.Web.Services;

namespace Todos.Web.Pages;

public class IndexBase : ComponentBase
{
    [Inject] private TodoItemService TodoItemService { get; set; }

    protected List<TodoItemViewModel>? PendingTodoItems { get; set; }
    protected List<TodoItemViewModel>? CompletedTodoItems { get; set; }
    protected bool IsLoading { get; set; }

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;

        var todoItems = await TodoItemService.GetTodoItemsAsync();
        PendingTodoItems = todoItems?.Where(x => x.Status == TodoItemStatus.Pending).ToList() ?? new();
        CompletedTodoItems = todoItems?.Where(x => x.Status == TodoItemStatus.Completed).ToList() ?? new();

        IsLoading = false;
        StateHasChanged();
    }
}