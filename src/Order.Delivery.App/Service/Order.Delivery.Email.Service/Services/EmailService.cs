using Order.Delivery.Email.Service.Builders;
using Order.Delivery.Email.Service.Models.Email;
using Order.Delivery.Email.Service.Models.Order;
using Order.Delivery.Email.Service.Services.Interfaces;
using System.Net.Mail;

namespace Order.Delivery.Email.Service.Services;

public class EmailService : IEmailService
{
    private readonly IConfiguration _configuration;

    public EmailService(IConfiguration configuration)
    {
        _configuration = configuration;
    }

    public async Task<EmailSendResult> SendOrderToEmailAsync(OrderMessage orderMessage)
    {
        try
        {
            var smtpHost = _configuration["Email:SmtpHost"];
            var smtpPort = int.Parse(_configuration["Email:SmtpPort"]!);
            var smtpUser = _configuration["Email:SmtpUser"];
            var smtpPass = _configuration["Email:SmtpPass"];
            var fromEmail = _configuration["Email:FromEmail"];

            using var smtp = new SmtpClient(smtpHost, smtpPort)
            {
                Credentials = new System.Net.NetworkCredential(smtpUser, smtpPass),
                EnableSsl = true
            };

            var emailDto = new EmailSendRequest()
            {
                To = orderMessage.Email,
                Subject = orderMessage.Action,
                Body = EmailBodyBuilder.GetNewOrderCreatedBody(orderMessage)
            };

            var mail = new MailMessage(fromEmail!, emailDto.To!, emailDto.Subject, emailDto.Body)
            {
                IsBodyHtml = true
            };

            
            await smtp.SendMailAsync(mail);
            

            return new() { Success = true };
        }
        catch (Exception ex)
        {
            return new() { Success = false, Error = ex.Message };
        }
    }
}
