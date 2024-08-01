using System.Text;
using System.Text.Json;
using RabbitMQ.Client;

namespace FormularAirline.API.Services;

public class MesssageProducer : IMessageProducer
{

    public void SendingMessages<T>(T message)
    {
        var factory = new ConnectionFactory()
        {
            HostName = "localhost",
            UserName = "user",
            Password = "mypass",
            VirtualHost = "/"
        };

        var con = factory.CreateConnection();

        using var channel = con.CreateModel();

        channel.QueueDeclare("booking", durable: true, exclusive: true);

        var jsonString = JsonSerializer.Serialize(message);
        var body = Encoding.UTF8.GetBytes(jsonString);

        channel.BasicPublish("", "booking", body: body);
    }
}