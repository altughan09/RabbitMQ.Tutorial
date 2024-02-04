using MassTransit;
using Microsoft.Extensions.Configuration;
using RabbitMQ.Consumer.Application;
using RabbitMQ.Consumer.Domain.Constants;

namespace RabbitMQ.Consumer;

public class Program
{
    static async Task Main(string[] args)
    {
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .Build();
        
        var rabbitMqHost = configuration["RabbitMq:Host"];
        var rabbitMqUser = configuration["RabbitMq:User"];
        var rabbitMqPass = configuration["RabbitMq:Pass"];

        var busControl = Bus.Factory.CreateUsingRabbitMq(cfg =>
        {
            cfg.Host(new Uri($"rabbitmq://{rabbitMqHost}"), h =>
            {
                h.Username(rabbitMqUser);
                h.Password(rabbitMqPass);
            });

            cfg.ReceiveEndpoint(Queue.BOOKINGS, (IRabbitMqReceiveEndpointConfigurator e) =>
            {
                e.Consumer<MessageConsumer>();
            });
        });

        await busControl.StartAsync();
        
        try
        {
            Console.WriteLine("Press enter to exit");
            await Task.Run(() => Console.ReadLine());
        }
        finally
        {
            await busControl.StopAsync();
        }
    }
}