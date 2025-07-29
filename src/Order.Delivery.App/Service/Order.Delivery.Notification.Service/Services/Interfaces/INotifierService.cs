using Order.Delivery.Notification.Service.Models;

namespace Order.Delivery.Notification.Service.Services.Interfaces;

public interface INotifierService
{
    void Notify(OrderMessage order);
}
