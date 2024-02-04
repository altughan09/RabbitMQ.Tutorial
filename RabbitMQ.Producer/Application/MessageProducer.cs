using System.Text;
using RabbitMQ.Client;
using RabbitMQ.Producer.Domain.Interfaces.Services;

namespace RabbitMQ.Producer.Application;

public class MessageProducer : IMessageProducer
{
    private readonly IConnectionFactory _connectionFactory;

    public MessageProducer(IConnectionFactory connectionFactory)
    {
        _connectionFactory = connectionFactory;
    }

    public void SendMessage<T>(T message, string queue)
    {
        using var connection = _connectionFactory.CreateConnection();
        using var channel = connection.CreateModel();

        var serializedMessage = System.Text.Json.JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(serializedMessage);

        channel.QueueDeclare(queue, true, false, false, null);
        channel.BasicPublish(string.Empty, queue, null, body);

        Console.WriteLine($"Sent message: {serializedMessage}");
    }
}