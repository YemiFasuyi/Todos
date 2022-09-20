using MediatR;
using Todos.Orchestrator.Todos.Models;

namespace Todos.Orchestrator.Todos.Queries;

public class GetTodoItemsQuery : IRequest<List<TodoItemViewModel>?> { }