namespace Order.Delivery.Notification.Service.Models;

public class Order
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
