using FluentValidation;

namespace Order.Delivery.App.Application.Commands.Orders.UpdateOrderStatus;

public class UpdateOrderStatusCommandValidator : AbstractValidator<UpdateOrderStatusCommand>
{
    public UpdateOrderStatusCommandValidator()
    {
        RuleFor(p => p.OrderId).NotNull().WithMessage("O id do pedido é obrigatório");
    }
}
