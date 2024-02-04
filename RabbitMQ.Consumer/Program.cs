using System.Text;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using RabbitMQ.Consumer.Domain.Constants;

namespace RabbitMQ.Consumer;

public class Program
{
    static void Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .Build();
        
        var rabbitMqHost = configuration["RabbitMq:Host"];
        var rabbitMqUser = configuration["RabbitMq:User"];
        var rabbitMqPass = configuration["RabbitMq:Pass"];

        var factory = new ConnectionFactory
        {
            HostName = rabbitMqHost,
            UserName = rabbitMqUser,
            Password = rabbitMqPass
        };

        using var connection = factory.CreateConnection();
        using var channel = connection.CreateModel();

        channel.QueueDeclare(queue: Queue.BOOKINGS, durable: true, exclusive: false, autoDelete: false, arguments: null);
        
        var consumer = new EventingBasicConsumer(channel);
        
        consumer.Received += (model, eventArgs) =>
        {
            var body = eventArgs.Body.ToArray();
            var message = Encoding.UTF8.GetString(body);
            Console.WriteLine($"Message received: {message}");
        };

        channel.BasicConsume(queue: Queue.BOOKINGS, autoAck: true, consumer: consumer);
        Console.ReadKey();
    }
}