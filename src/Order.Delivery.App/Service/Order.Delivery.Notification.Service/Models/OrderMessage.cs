using Entity = Order.Delivery.Notification.Service.Models;

namespace Order.Delivery.Notification.Service.Models;

public class OrderMessage
{
    public string Action { get; set; } = string.Empty;
    public string? Email { get; set; }
    public Entity.Order? Order { get; set; }
}
