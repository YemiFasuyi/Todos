using MediatR;
using Todos.Application.Interfaces;
using Todos.Orchestrator.TodoItems.Commands;

namespace Todos.Application.TodoItems.Commands;

public class CreateTodoItemsCommandHandler : IRequestHandler<CreateTodoItemCommand, int>
{
    private readonly ITodoItemRepository _todoItemRepository;

    public CreateTodoItemsCommandHandler(ITodoItemRepository todoItemRepository)
    {
        _todoItemRepository = todoItemRepository;
    }

    public async Task<int> Handle(CreateTodoItemCommand request, CancellationToken cancellationToken)
    {
        return await _todoItemRepository.CreateTodoItemAsync(request.Description, Domain.TodoItems.TodoItemStatusEnum.Pending, cancellationToken);
    }
}