namespace Order.Delivery.App.Application.Models;

public record ResponseBase<T>
{
    public bool Success { get; set; }
    public T? Result { get; set; }
    public string? ErrorMessage { get; set; }
}
