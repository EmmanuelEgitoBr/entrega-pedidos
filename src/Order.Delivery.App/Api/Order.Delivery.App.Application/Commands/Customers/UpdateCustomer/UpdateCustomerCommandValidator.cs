using FluentValidation;

namespace Order.Delivery.App.Application.Commands.Customers.UpdateCustomer;

public class UpdateCustomerCommandValidator : AbstractValidator<UpdateCustomerCommand>
{
    public UpdateCustomerCommandValidator()
    {
        RuleFor(p => p.Email).NotNull().WithMessage("O email é obrigatório");
        RuleFor(p => p.PhoneNumber).NotNull().WithMessage("O telefone é obrigatório");
        RuleFor(p => p.CustomerId).NotNull().WithMessage("O nome é obrigatório");
    }
}
