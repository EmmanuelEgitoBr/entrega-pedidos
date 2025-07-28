namespace Order.Delivery.App.Application.Services.Interfaces;
using Entity = Order.Delivery.App.Domain.Aggregates;

public interface IPublisherService
{
    Task PublishMessageToTopicAsync(Entity.Order order);
}
