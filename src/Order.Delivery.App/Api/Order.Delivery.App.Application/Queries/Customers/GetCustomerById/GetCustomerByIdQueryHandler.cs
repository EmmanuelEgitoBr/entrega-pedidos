using AutoMapper;
using MediatR;
using Order.Delivery.App.Application.Dtos;
using Order.Delivery.App.Application.Models;
using Order.Delivery.App.Domain.Interfaces;

namespace Order.Delivery.App.Application.Queries.Customers.GetCustomerById;

public class GetCustomerByIdQueryHandler : IRequestHandler<GetCustomerByIdQuery, ResponseBase<CustomerDto>>
{
    private readonly ICustomerRepository _customerRepository;
    private readonly IMapper _mapper;

    public GetCustomerByIdQueryHandler(ICustomerRepository customerRepository,
        IMapper mapper)
    {
        _customerRepository = customerRepository;
        _mapper = mapper;
    }

    public async Task<ResponseBase<CustomerDto>> Handle(GetCustomerByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            var customerEntity = await _customerRepository.GetCustomerByIdAsync(request.CustomerId);

            if (customerEntity is null)
            {
                return new ResponseBase<CustomerDto>
                {
                    Success = false,
                    ErrorMessage = "Não foi possível encontrar o cliente"
                };
            }

            var customerDto = _mapper.Map<CustomerDto>(customerEntity);

            ResponseBase<CustomerDto> response = new()
            {
                Success = true,
                Result = customerDto
            };

            return response;
        }
        catch (Exception ex)
        {
            return new ResponseBase<CustomerDto>
            {
                Success = false,
                ErrorMessage = ex.Message
            };
        }

        

        throw new NotImplementedException();
    }
}
