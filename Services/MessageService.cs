using System.Text;
using System.Text.Json;
using Microsoft.Extensions.Options;
using Pix.Config;
using Pix.DTOs;
using RabbitMQ.Client;

namespace Pix.Services;

public class MessageService(IOptions<QueueConfig> queueConfig)
{
    private readonly string _hostName = queueConfig.Value.HostName;

    public void SendMessage(object obj, string queue){
        ConnectionFactory factory = new ()
        {
            HostName = _hostName
        };

        IConnection connection = factory.CreateConnection();
        using IModel channel = connection.CreateModel();

        channel.QueueDeclare(
            queue: queue,
            durable: true,
            exclusive: false,
            autoDelete: false,
            arguments: null
        );

        string json = JsonSerializer.Serialize(obj);
        var body = Encoding.UTF8.GetBytes(json);

        IBasicProperties basicProperties = channel.CreateBasicProperties();
        basicProperties.Persistent = true;

        channel.BasicPublish(
            exchange: string.Empty,
            routingKey : queue,
            basicProperties: null,
            body: body
        );
    }
}