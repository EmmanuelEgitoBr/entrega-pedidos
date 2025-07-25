using System.ComponentModel.DataAnnotations.Schema;

namespace Order.Delivery.App.Domain.Entities;

public class Item
{
    public int ItemId { get; set; }
    public string? OrderId { get; set; }
    public int ProductId { get; set; }
    public int Count { get; set; }
    [NotMapped]
    public Product? Product { get; set; }
}
