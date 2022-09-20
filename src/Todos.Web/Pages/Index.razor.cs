using Microsoft.AspNetCore.Components;
using Todos.Orchestrator.TodoItems.Models;
using Todos.Web.Services;

namespace Todos.Web.Pages;

public class IndexBase : ComponentBase
{
    [Inject] private TodoItemService TodoItemService { get; set; }

    protected List<TodoItemViewModel>? PendingTodoItems { get; set; }
    protected List<TodoItemViewModel>? CompletedTodoItems { get; set; }
    protected bool IsLoading { get; set; }
    protected bool IsAddNewDrawerOpened { get; set; }
    protected bool IsEditDrawerOpened { get; set; }
    protected int CurrentTodoItemId { get; set; }

    protected override async Task OnInitializedAsync()
    {
        IsLoading = true;

        await PopulateTodoItemsAsync();

        IsLoading = false;
        StateHasChanged();
    }

    private async Task PopulateTodoItemsAsync()
    {
        var todoItems = await TodoItemService.GetTodoItemsAsync();
        PendingTodoItems = todoItems?.Where(x => x.Status == TodoItemStatus.Pending).ToList() ?? new();
        CompletedTodoItems = todoItems?.Where(x => x.Status == TodoItemStatus.Completed).ToList() ?? new();
    }

    protected void AddNew()
    {
        IsAddNewDrawerOpened = true;
    }

    protected void Edit(int id)
    {
        CurrentTodoItemId = id;
        IsEditDrawerOpened = true;
    }

    protected void NewDrawerOpenedChanged(bool isOpen)
    {
        IsAddNewDrawerOpened = isOpen;
        StateHasChanged();
    }

    protected void EditDrawerOpenedChanged(bool isOpen)
    {
        IsEditDrawerOpened = isOpen;
        StateHasChanged();
    }

    protected async Task OnTodoItemSavedAsync()
    {
        await PopulateTodoItemsAsync();
    }
}