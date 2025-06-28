using System;
using System.Text;
using System.Threading.Tasks;
using DataCollection.BcrPostgressSQL.BCR_PsqlItems;
using DataCollection.Editor.PostgreSQL.BCR_PsqlItems;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using UnityEngine;

namespace DataCollection.BcrRabbitMQ
{
    [DefaultExecutionOrder(-857)]
    public class Bcr_RabbitMQProducer : PersistentSingleton<Bcr_RabbitMQProducer>
    {
        private IConnection _connection;

        private IChannel _channel;

        //TOD YAML
        private const string ExchangeName = "";
        private const string RoutingKey = "";
        private const string QueueName = "";

        private async Task CreateConnection()
        {
            ConnectionFactory factory = new ConnectionFactory
            {
                HostName = "" //TODO YAML
            };

            _connection = await factory.CreateConnectionAsync();
            _channel = await _connection.CreateChannelAsync();
            await CreateQueueAndExchange();
        }

        private async Task CreateQueueAndExchange()
        {
            await _channel.ExchangeDeclareAsync(ExchangeName, ExchangeType.Direct);
            await _channel.QueueDeclareAsync(QueueName, false, false, false, null);
            await _channel.QueueBindAsync(QueueName, ExchangeName, RoutingKey, null);
        }

        private async void CloseConnection()
        {
            await _channel.CloseAsync();
            await _connection.CloseAsync();
        }

        [ContextMenu("Send to rabbitMQ")]
        public async Task Send(string message)
        {
            await CheckIfConnected();
            byte[] messageBodyBytes = Encoding.UTF8.GetBytes(message);


            var publishTask = _channel.BasicPublishAsync(ExchangeName, RoutingKey, messageBodyBytes);
            await publishTask;

            Debug.Log($"Sent to RabbitMQ. Completed successfully: {publishTask.IsCompletedSuccessfully}");
        }

     
        
        private async Task CheckIfConnected()
        {
            if (_connection == null)
            {
                await  CreateConnection();
                return;
            }

            while (!_connection.IsOpen)
            {
                await Task.Delay(100);
            }
            
        }
    }
}