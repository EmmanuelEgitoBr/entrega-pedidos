using FluentValidation;

namespace Order.Delivery.App.Application.Commands.Products.CreateProduct;

public class CreateProductCommandValidator : AbstractValidator<CreateProductCommand>
{
    public CreateProductCommandValidator()
    {
        RuleFor(p => p.Description).NotEmpty().WithMessage("A descrição não pode ser vazia");
        RuleFor(p => p.Price).NotEmpty().WithMessage("O preço não pode ser vazio");
        RuleFor(p => p.Name).NotEmpty().WithMessage("O nome do produto não pode ser vazio");
    }
}
