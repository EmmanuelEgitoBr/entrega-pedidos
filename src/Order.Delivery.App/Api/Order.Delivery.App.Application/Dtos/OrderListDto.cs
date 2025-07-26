namespace Order.Delivery.App.Application.Dtos;

public class OrderListDto
{
    public IList<OrderDto>? Orders { get; set; }
    public CustomerDto Customer { get; set; } = new CustomerDto();
}
