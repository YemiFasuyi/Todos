using MediatR;
using Todos.Orchestrator.TodoItems.Models;

namespace Todos.Orchestrator.TodoItems.Queries;

public class GetTodoItemsQuery : IRequest<List<TodoItemViewModel>?> { }