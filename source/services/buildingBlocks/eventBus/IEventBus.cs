namespace EventBus;
public interface IEventBus
{
    void Publish<T>(T @event, string queueName);
    void SubscribeWithHandler<T>(string queueName);
}