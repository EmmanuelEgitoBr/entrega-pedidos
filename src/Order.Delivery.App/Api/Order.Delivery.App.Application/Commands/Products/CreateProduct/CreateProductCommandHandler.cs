using MediatR;
using Order.Delivery.App.Application.Models;
using Order.Delivery.App.Domain.Entities;
using Order.Delivery.App.Domain.Interfaces;

namespace Order.Delivery.App.Application.Commands.Products.CreateProduct;

public class CreateProductCommandHandler : IRequestHandler<CreateProductCommand, ResponseBase<int>>
{
    private readonly IProductRepository _productRepository;

    public CreateProductCommandHandler(IProductRepository productRepository)
    {
        _productRepository = productRepository;
    }

    public async Task<ResponseBase<int>> Handle(CreateProductCommand request, CancellationToken cancellationToken)
    {
        try
        {
            var product = new Product
            {
                Name = request.Name,
                Description = request.Description,
                Price = request.Price
            };

            var productEntity = await _productRepository.CreateProductAsync(product);

            if (productEntity is null)
            {
                return new ResponseBase<int>
                {
                    Success = false,
                    ErrorMessage = "Não foi possível criar o produto"
                };
            }

            ResponseBase<int> response = new()
            {
                Success = true,
                Result = productEntity.ProductId
            };

            return response;
        }
        catch (Exception ex)
        {
            return new ResponseBase<int>
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }
}
