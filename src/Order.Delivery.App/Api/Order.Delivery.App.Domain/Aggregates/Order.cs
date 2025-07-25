using Order.Delivery.App.Domain.Entities;
using Order.Delivery.App.Domain.Enums;

namespace Order.Delivery.App.Domain.Aggregates;

public class Order
{
    public string OrderId { get; set; }
    public DateTime OrderCreateDate { get; set; }
    public DateTime OrderLastUpdate { get; set; }
    public string OrderSituation { get; set; }
    public IList<Item>? Items { get; set; }
    public int CustomerId { get; set; }
    public Customer? Customer { get; set; }
    public double TotalPrice { get; set; }

    public Order(IList<Item> items, Customer customer)
    {
        OrderId = Guid.NewGuid().ToString();
        OrderCreateDate = DateTime.Now;
        OrderLastUpdate = OrderCreateDate;
        OrderSituation = OrderStatus.CREATED.ToString();
        Items = items;
        Customer = customer;
        CustomerId = customer.CustomerId;
    }

    public Order()
    {

    }
}
