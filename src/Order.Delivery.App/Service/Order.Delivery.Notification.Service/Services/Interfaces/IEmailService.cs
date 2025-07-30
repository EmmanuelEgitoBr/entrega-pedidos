using Order.Delivery.Notification.Service.Models;
using Order.Delivery.Notification.Service.Models.Email;
using Refit;

namespace Order.Delivery.Notification.Service.Services.Interfaces;

public interface IEmailService
{
    [Post("/api/v1/emails/send-email")]
    Task<ApiResponse<EmailSendResult>> SendEmailAsync([Body] OrderMessage message);
}
