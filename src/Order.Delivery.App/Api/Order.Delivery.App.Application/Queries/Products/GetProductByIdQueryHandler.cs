using AutoMapper;
using MediatR;
using Order.Delivery.App.Application.Dtos;
using Order.Delivery.App.Application.Models;
using Order.Delivery.App.Domain.Interfaces;
using Order.Delivery.App.Infra.Repositories;

namespace Order.Delivery.App.Application.Queries.Products;

public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, ResponseBase<ProductDto>>
{
    private readonly IProductRepository _productRepository;
    private readonly IMapper _mapper;

    public GetProductByIdQueryHandler(IProductRepository productRepository,
        IMapper mapper)
    {
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<ResponseBase<ProductDto>> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var productEntity = await _productRepository.GetProductByIdAsync(request.ProductId);

            if (productEntity is null)
            {
                return new ResponseBase<ProductDto>
                {
                    Success = false,
                    ErrorMessage = "Não foi possível encontrar o produto"
                };
            }

            var productDto = _mapper.Map<ProductDto>(productEntity);

            ResponseBase<ProductDto> response = new()
            {
                Success = true,
                Result = productDto
            };

            return response;
        }
        catch (Exception ex)
        {
            return new ResponseBase<ProductDto>
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }
    }
}
