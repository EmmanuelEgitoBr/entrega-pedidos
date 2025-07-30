using Entity = Order.Delivery.Email.Service.Models.Order;

namespace Order.Delivery.Email.Service.Models.Order;

public class OrderMessage
{
    public string Action { get; set; } = string.Empty;
    public string? Email { get; set; }
    public Entity.Order? Order { get; set; }
}
