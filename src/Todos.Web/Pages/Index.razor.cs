﻿using Microsoft.AspNetCore.Components;
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

    protected void NewDrawerOpenedChanged(bool isOpen)
    {
        IsAddNewDrawerOpened = isOpen;
        StateHasChanged();
    }

    protected async Task OnTodoItemSavedAsync(int id)
    {
        await PopulateTodoItemsAsync();
    }
}