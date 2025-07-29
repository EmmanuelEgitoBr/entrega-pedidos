using Entity = Order.Delivery.App.Domain.Aggregates;

namespace Order.Delivery.App.Application.Models;

public class OrderMessage
{
    public string Action { get; set; } = string.Empty;
    public string? Email { get; set; }
    public Entity.Order? Order { get; set; } 
}
