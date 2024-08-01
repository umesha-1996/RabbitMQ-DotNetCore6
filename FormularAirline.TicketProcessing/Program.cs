// See https://aka.ms/new-console-template for more information
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System.Text;

Console.WriteLine("Hello to the ticketing service");

var factory = new ConnectionFactory()
{
    HostName = "localhost",
    UserName = "user",
    Password = "mypass",
    VirtualHost = "/"
};

var con = factory.CreateConnection();

using var channel = con.CreateModel();

channel.QueueDeclare("bookings", durable: true, exclusive: true);

var consumer = new EventingBasicConsumer(channel);

consumer.Received += (model, eventArgs) =>
{
    //getting my byte[]
    var body = eventArgs.Body.ToArray();

    var message = Encoding.UTF8.GetString(body);

    Console.WriteLine($"new ticket processing is initiate - {message}");
};

channel.BasicConsume("bookings", true, consumer);

Console.ReadKey();
