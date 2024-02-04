namespace RabbitMQ.Producer.Domain.Interfaces.Services;

public interface IMessageProducer
{
    public void SendMessage<T>(T message, string queueName);
}