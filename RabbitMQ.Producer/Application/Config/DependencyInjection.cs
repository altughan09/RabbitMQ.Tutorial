using RabbitMQ.Producer.Domain.Interfaces.Services;

namespace RabbitMQ.Producer.Application.Config;

public static class DependencyInjection
{
    public static IServiceCollection AddServiceCollection(this IServiceCollection services)
    {
        services.AddScoped<IMessageProducer, MessageProducer>();
        return services;
    }
}