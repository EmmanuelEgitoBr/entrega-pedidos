using FluentValidation;

namespace Order.Delivery.App.Application.Commands.Items.CreateItem;

public class CreateItemCommandValidator : AbstractValidator<CreateItemCommand>
{
    public CreateItemCommandValidator()
    {
        RuleFor(p => p.ProductId).NotNull().WithMessage("O id do produto não pode ser nulo");
        RuleFor(p => p.Count).GreaterThanOrEqualTo(0).WithMessage("A qauntidade deve ser maior ou igual a zero");
    }
}
