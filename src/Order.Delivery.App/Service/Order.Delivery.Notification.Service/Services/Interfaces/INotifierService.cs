namespace Order.Delivery.Notification.Service.Services.Interfaces;
using Entity = Order.Delivery.Notification.Service.Models;

public interface INotifierService
{
    void Notify(Entity.Order order);
}
