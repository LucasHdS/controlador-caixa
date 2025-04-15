namespace EventBus;
public interface IEventBus
{
    void Publish<T>(T @event, string queueName);
    void Subscribe<T>(string queueName);
}