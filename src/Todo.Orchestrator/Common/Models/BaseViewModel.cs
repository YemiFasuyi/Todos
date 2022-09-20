namespace Todos.Orchestrator.Common.Models;

public class BaseViewModel
{
    public int Id { get; set; }
    public DateTime Created { get; set; }
    public DateTime Updated { get; set; }
    public DateTime? Deleted { get; set; }
}