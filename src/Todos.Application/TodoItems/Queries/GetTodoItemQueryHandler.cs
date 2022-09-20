using MediatR;
using Todos.Application.Interfaces;
using Todos.Orchestrator.TodoItems.Models;
using Todos.Orchestrator.TodoItems.Queries;

namespace Todos.Application.TodoItems.Queries;

public class GetTodoItemQueryHandler : IRequestHandler<GetTodoItemQuery, TodoItemViewModel?>
{
    private readonly ITodoItemRepository _todoItemRepository;

    public GetTodoItemQueryHandler(ITodoItemRepository todoItemRepository)
    {
        _todoItemRepository = todoItemRepository;
    }

    public async Task<TodoItemViewModel?> Handle(GetTodoItemQuery request, CancellationToken cancellationToken)
    {
        var todoItem = await _todoItemRepository.GetTodoItemAsync(request.Id, cancellationToken);
        if (todoItem == null)
            return null;

        return new()
        {
            Id = todoItem.Id,
            Description = todoItem.Description,
            Status = (TodoItemStatus)todoItem.Status,
            Created = todoItem.Created,
            Updated = todoItem.Updated
        };
    }
}