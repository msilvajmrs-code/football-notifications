using RabbitMQ.Client;
using System;
using System.Text.Json;
using Visma.Technical.Core.Contracts;

namespace Visma.Technical.Core.Infrastructure.Messaging
{
    public class RabbitMqPublisher : IMqPublisher
    {
        private readonly ConnectionFactory _factory;

        public RabbitMqPublisher(string rabbitMqUri)
        {
            _factory = new ConnectionFactory
            {
                Uri = new Uri(rabbitMqUri)
            };
        }

        public async Task PublishAsync<T>(string queue, T message)
        {
            if (string.IsNullOrWhiteSpace(queue))
                throw new ArgumentException("Queue name must be provided.", nameof(queue));

            var body = JsonSerializer.SerializeToUtf8Bytes(message);

            await using var connection = await _factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.QueueDeclareAsync(
                queue: queue,
                durable: true,
                exclusive: false,
                autoDelete: false,
                arguments: null);


            await channel.BasicPublishAsync(
                exchange: string.Empty,
                routingKey: queue,
                body: body);
        }

    }
}