using FluentValidation;

namespace Order.Delivery.App.Application.Queries.Items.GetItemByOrderId;

public class GetItemByOrderIdQueryValidator : AbstractValidator<GetItemByOrderIdQuery>
{
	public GetItemByOrderIdQueryValidator()
	{
        RuleFor(p => p.OrderId).NotNull().WithMessage("O id é obrigatório");
    }
}
