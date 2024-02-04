using MassTransit;
using Newtonsoft.Json;
using RabbitMQ.SharedProject;

namespace RabbitMQ.Consumer.Application;

public class MessageConsumer: IConsumer<Booking>
{
    public async Task Consume(ConsumeContext<Booking> context)
    {
        var jsonMessage = JsonConvert.SerializeObject(context.Message);
        Console.WriteLine($"Message received: {jsonMessage}");
    }
}