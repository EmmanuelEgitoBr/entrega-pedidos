using Order.Delivery.Notification.Service.Services.Interfaces;
using Entity = Order.Delivery.Notification.Service.Models;

namespace Order.Delivery.Notification.Service.Services;

public class NotifierService : INotifierService
{
    public void Notify(Entity.Order order)
    {
        SendEmail(order!.Customer!.Email);
    }

    private void SendEmail(string email)
    {
        Console.WriteLine("Email sent to recipient: " + email);
    }
}
