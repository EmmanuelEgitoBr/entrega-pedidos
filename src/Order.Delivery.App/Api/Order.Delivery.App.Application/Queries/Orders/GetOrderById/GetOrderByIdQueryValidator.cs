using FluentValidation;

namespace Order.Delivery.App.Application.Queries.Orders.GetOrderById;

public class GetOrderByIdQueryValidator : AbstractValidator<GetOrderByIdQuery>
{
    public GetOrderByIdQueryValidator()
    {
        RuleFor(p => p.OrderId).NotNull().WithMessage("O id do pedido é obrigatório");
    }
}
