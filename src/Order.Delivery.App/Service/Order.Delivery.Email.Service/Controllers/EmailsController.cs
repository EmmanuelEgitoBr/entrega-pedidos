using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Order.Delivery.Email.Service.Models.Email;
using Order.Delivery.Email.Service.Models.Order;
using Order.Delivery.Email.Service.Services.Interfaces;

namespace Order.Delivery.Email.Service.Controllers
{
    [Route("api/v1/emails")]
    [ApiController]
    public class EmailsController : ControllerBase
    {
        private readonly IEmailService _emailService;

        public EmailsController(IEmailService emailService)
        {
            _emailService = emailService;
        }

        /// <summary>
        /// Endpoint para envio de email com as informações do pedido vindo do worker
        /// </summary>
        /// <param name="orderMessage"></param>
        /// <returns></returns>
        [HttpPost("send-email")]
        public async Task<ActionResult<EmailSendResult>> SendEmail([FromBody] OrderMessage orderMessage)
        {
            var result = await _emailService.SendOrderToEmailAsync(orderMessage);

            if (result.Success) { return Ok(result); }

            return StatusCode(500, new { Success = false, Error = result.Error });
        }
    }
}
