using System.Text;
using Consumer;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;


Connection localConnection = new Connection();

var factory = new ConnectionFactory { HostName = "HOST_NAME" };
using var connection = await factory.CreateConnectionAsync();
using var channel = await connection.CreateChannelAsync();

const string exchangeName = "EXCHANGE_NAME";
const string routingKey = "ROUTING_KEY";
const string queueName = "QUEUE_NAME";

await channel.ExchangeDeclareAsync(exchangeName, ExchangeType.Direct);
await channel.QueueDeclareAsync(queue: queueName, durable: false, exclusive: false, autoDelete: false,
    arguments: null);
await channel.QueueBindAsync(queueName, exchangeName, routingKey, null);

Console.WriteLine(" [SET UP] Start up completed - Waiting for messages.");

var consumer = new AsyncEventingBasicConsumer(channel);

localConnection.StartConnection();

consumer.ReceivedAsync += async (model, ea) =>
{
    
    Console.WriteLine(" [LOG] Message received");
    var body = ea.Body.ToArray();
    var message = Encoding.UTF8.GetString(body);
    await localConnection.Upload(message);
};

await channel.BasicConsumeAsync(queueName, autoAck: true, consumer: consumer);

Console.ReadLine();

