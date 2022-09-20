using MediatR;
using Todos.Application.Interfaces;
using Todos.Domain.TodoItems;
using Todos.Orchestrator.TodoItems.Commands;

namespace Todos.Application.TodoItems.Commands;

public class UpdateTodoItemStatusCommandHandler : IRequestHandler<UpdateTodoItemStatusCommand, bool>
{
    private readonly ITodoItemRepository _todoItemRepository;

    public UpdateTodoItemStatusCommandHandler(ITodoItemRepository todoItemRepository)
    {
        _todoItemRepository = todoItemRepository;
    }

    public async Task<bool> Handle(UpdateTodoItemStatusCommand request, CancellationToken cancellationToken)
    {
        var todoItem = await _todoItemRepository.GetTodoItemAsync(request.Id, cancellationToken);
        if (todoItem == null)
            return false;

        var status = (TodoItemStatusEnum)request.Status;
        if (todoItem.Status == status)
            return false;

        await _todoItemRepository.UpdateTodoItemAsync(todoItem.Id, todoItem.Description, status, cancellationToken);
        return true;
    }
}