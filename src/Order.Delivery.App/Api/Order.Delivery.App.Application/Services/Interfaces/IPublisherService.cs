namespace Order.Delivery.App.Application.Services.Interfaces;

using Order.Delivery.App.Application.Models;

public interface IPublisherService
{
    Task PublishMessageToTopicAsync(OrderMessage order);
}
