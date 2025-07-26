using FluentValidation;

namespace Order.Delivery.App.Application.Queries.Orders.GetOrderByCustomerId;

public class GetOrderByCustomerIdQueryValidator : AbstractValidator<GetOrderByCustomerIdQuery>
{
    public GetOrderByCustomerIdQueryValidator()
    {
        RuleFor(p => p.CustomerId).NotNull().WithMessage("O id do cliente é obrigatório");
        RuleFor(p => p.CustomerId).NotEmpty().WithMessage("O id do cliente é obrigatório");
    }
}
