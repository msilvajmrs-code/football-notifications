using RabbitMQ.Client;
using RabbitMQ.Client.Events;
using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;
using Visma.Technical.Core.Contracts;

namespace Visma.Technical.Core.Infrastructure.Messaging
{
    public class RabbitMqSubscriber : IMqSubscriber, IDisposable
    {
        private IConnection? _connection;
        private readonly ConnectionFactory _connectionFactory;

        public RabbitMqSubscriber(string rabbitMqUri)
        {
            _connectionFactory = new ConnectionFactory
                {
                    Uri = new Uri(rabbitMqUri)
                };
        }

        public async Task SubscribeAsync<T>(string topic, Func<T?, Task> onMessageReceived)
        {
            if (string.IsNullOrWhiteSpace(topic))
                throw new ArgumentException("Topic name must be provided.", nameof(topic));

            _connection ??= await _connectionFactory.CreateConnectionAsync();
            var channel = await _connection.CreateChannelAsync();
            var queueDeclareResult = await channel.QueueDeclareAsync();
            if (queueDeclareResult is null)
            {
                throw new InvalidOperationException("Failed to declare a queue.");
            }
            // Just to make sure it works if the subscriber starts before the publisher
            await channel.ExchangeDeclareAsync(exchange: topic, type: ExchangeType.Fanout);
            await channel.QueueBindAsync(
                queue: queueDeclareResult.QueueName, 
                exchange: topic, 
                routingKey: string.Empty);

            var consumer = new AsyncEventingBasicConsumer(channel);
            consumer.ReceivedAsync += async (model, ea) =>
            {
                if (ea.Body.Length == 0)
                {
                    await onMessageReceived(default);
                    return;
                }
                byte[] body = ea.Body.ToArray();
                var message = Encoding.UTF8.GetString(body);
                await onMessageReceived(JsonSerializer.Deserialize<T>(message));
            };
            await channel.BasicConsumeAsync(queueDeclareResult.QueueName, autoAck: true, consumer: consumer);
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

    }
}
