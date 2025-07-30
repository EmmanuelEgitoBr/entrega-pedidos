using Order.Delivery.Email.Service.Models.Email;
using Order.Delivery.Email.Service.Models.Order;

namespace Order.Delivery.Email.Service.Services.Interfaces;

public interface IEmailService
{
    Task<EmailSendResult> SendOrderToEmailAsync(OrderMessage orderMessage);
}
