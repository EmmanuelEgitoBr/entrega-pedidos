using FluentValidation;

namespace Order.Delivery.App.Application.Queries.Products;

public class GetProductByIdQueryValidator : AbstractValidator<GetProductByIdQuery>
{
    public GetProductByIdQueryValidator()
    {
        RuleFor(p => p.ProductId).NotNull().WithMessage("O id é obrigatório");
    }
}
