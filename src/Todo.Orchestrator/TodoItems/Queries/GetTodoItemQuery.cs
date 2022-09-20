using MediatR;
using Todos.Orchestrator.TodoItems.Models;

namespace Todos.Orchestrator.TodoItems.Queries;

public class GetTodoItemQuery : IRequest<TodoItemViewModel>
{
    public int Id { get; set; }
}