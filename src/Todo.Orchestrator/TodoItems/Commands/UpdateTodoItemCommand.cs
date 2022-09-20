using FluentValidation;
using MediatR;
using Todos.Orchestrator.TodoItems.Models;

namespace Todos.Orchestrator.TodoItems.Commands;

public class UpdateTodoItemStatusCommand : IRequest<bool>
{
    public int Id { get; set; }
    public TodoItemStatus Status { get; set; }
}

public class UpdateTodoItemStatusCommandValidator : AbstractValidator<UpdateTodoItemStatusCommand>
{
    public UpdateTodoItemStatusCommandValidator()
    {
        RuleFor(x => x.Id).NotEmpty().WithMessage("Id is missing");
        RuleFor(x => x.Status).IsInEnum().WithMessage("Status is incorrect");
    }
}