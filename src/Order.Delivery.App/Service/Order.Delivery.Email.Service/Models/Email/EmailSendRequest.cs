namespace Order.Delivery.Email.Service.Models.Email;

public class EmailSendRequest
{
    public string? To { get; set; }
    public string? Subject { get; set; }
    public string? Body { get; set; }
}
