using RabbitMQ.Client;
using RabbitMQ.Producer.Application.Config;

namespace RabbitMQ.Producer;

public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);
        
        var configuration = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory())
            .AddJsonFile("appsettings.json", false, true)
            .AddEnvironmentVariables()
            .Build();
        
        builder.Services.AddControllers();
        
        // Add services to the container.
        builder.Services.AddAuthorization();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();
        
        var rabbitMqHost = configuration.GetValue<string>("RabbitMq:Host");
        var rabbitMqUser = configuration.GetValue<string>("RabbitMq:User");
        var rabbitMqPass = configuration.GetValue<string>("RabbitMq:Pass");

        builder.Services.AddSingleton<IConnectionFactory, ConnectionFactory>(_ => new ConnectionFactory
        {
            HostName = rabbitMqHost,
            UserName = rabbitMqUser,
            Password = rabbitMqPass
        });
        
        // DI
        builder.Services.AddServiceCollection();
        
        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();

        app.MapControllers();
        
        app.Run();
    }
}