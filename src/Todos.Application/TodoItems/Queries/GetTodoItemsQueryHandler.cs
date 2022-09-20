using MediatR;
using Todos.Application.Interfaces;
using Todos.Orchestrator.TodoItems.Models;
using Todos.Orchestrator.TodoItems.Queries;

namespace Todos.Application.TodoItems.Queries;

public class GetTodoItemsQueryHandler : IRequestHandler<GetTodoItemsQuery, List<TodoItemViewModel>?>
{
    private readonly ITodoItemRepository _todoItemRepository;

    public GetTodoItemsQueryHandler(ITodoItemRepository todoItemRepository)
    {
        _todoItemRepository = todoItemRepository;
    }

    public async Task<List<TodoItemViewModel>?> Handle(GetTodoItemsQuery request, CancellationToken cancellationToken)
    {
        var todoItems = await _todoItemRepository.GetTodoItemsAsync(cancellationToken);
        if (todoItems == null || !todoItems.Any())
            return null;

        return todoItems.Select(x => new TodoItemViewModel
        {
            Id = x.Id,
            Description = x.Description,
            Status = (TodoItemStatus)x.Status,
            Created = x.Created,
            Updated = x.Updated,
        }).ToList();
    }
}