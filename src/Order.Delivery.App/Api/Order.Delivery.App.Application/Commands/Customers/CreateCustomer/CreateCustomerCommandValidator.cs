using FluentValidation;

namespace Order.Delivery.App.Application.Commands.Customers.CreateCustomer;

public class CreateCustomerCommandValidator : AbstractValidator<CreateCustomerCommand>
{
    public CreateCustomerCommandValidator()
    {
        RuleFor(p => p.Email).NotEmpty().WithMessage("O email não pode ser vazio");
        RuleFor(p => p.PhoneNumber).NotEmpty().WithMessage("O número de telefone não pode ser vazio");
        RuleFor(p => p.Name).NotEmpty().WithMessage("O nome do cliente não pode ser vazio");
    }
}
