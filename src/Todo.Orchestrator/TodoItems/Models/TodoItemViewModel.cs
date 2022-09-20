using Todos.Orchestrator.Common.Models;

namespace Todos.Orchestrator.Todos.Models;

public class TodoItemViewModel : BaseViewModel
{
    public string Description { get; set; }
    public TodoItemStatus Status { get; set; }
}