namespace Order.Delivery.App.Domain.Entities;

public class Item
{
    public int ItemId { get; set; }
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
}
