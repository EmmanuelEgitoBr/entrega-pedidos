namespace Order.Delivery.App.Application.Dtos;

public class ItemDto
{
    public int ItemId { get; set; }
    public string? OrderId { get; set; }
    public ProductDto? Product { get; set; }
    public int Count { get; set; }
}
