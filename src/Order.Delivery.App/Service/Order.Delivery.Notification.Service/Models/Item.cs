namespace Order.Delivery.Notification.Service.Models;

public class Item
{
    public int ItemId { get; set; }
    public string? OrderId { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
    public Product? Product { get; set; }
}
