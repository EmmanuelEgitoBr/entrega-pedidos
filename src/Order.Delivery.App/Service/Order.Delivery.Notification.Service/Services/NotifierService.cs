using Order.Delivery.Notification.Service.Models;
using Order.Delivery.Notification.Service.Services.Interfaces;

namespace Order.Delivery.Notification.Service.Services;

public class NotifierService : INotifierService
{
    public void Notify(OrderMessage order)
    {
        SendEmail(order!.Email!);
    }

    private void SendEmail(string email)
    {
        Console.WriteLine("Email sent to recipient: " + email);
    }
}
