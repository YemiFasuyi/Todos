using FluentValidation;
using MediatR;

namespace Todos.Orchestrator.Todos.Commands;

public class CreateTodoItemCommand : IRequest<int>
{
    public string Description { get; set; }
}

public class CreateTodoItemCommandValidator : AbstractValidator<CreateTodoItemCommand>
{
    public CreateTodoItemCommandValidator()
    {
        RuleFor(x => x.Description).NotEmpty().WithMessage("Description is missing");
    }
}