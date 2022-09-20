namespace Todos.Domain.TodoItems;

public class TodoItem : BaseEntity
{
    public string Description { get; set; }
    public TodoItemStatusEnum Status { get; set; }
}