using FluentValidation;

namespace Order.Delivery.App.Application.Queries.Customers.GetCustomerById
{
    public class GetCustomerByIdQueryValidator : AbstractValidator<GetCustomerByIdQuery>
    {
        public GetCustomerByIdQueryValidator()
        {
            RuleFor(p => p.CustomerId).NotNull().WithMessage("O id é obrigatório");
        }
    }
}
