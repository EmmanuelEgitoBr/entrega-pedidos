using Order.Delivery.App.Domain.Entities;

namespace Order.Delivery.App.Application.Dtos;

public class OrderDto
{
    public string OrderId { get; set; }
    public DateTime OrderCreateDate { get; set; }
    public DateTime OrderLastUpdate { get; set; }
    public string OrderSituation { get; set; }
    public IList<Item>? Items { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public decimal TotalPrice { get; set; }
}
