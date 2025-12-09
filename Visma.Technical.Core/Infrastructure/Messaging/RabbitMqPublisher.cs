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

        public async Task PublishAsync<T>(string topic, T message)
        {
            if (string.IsNullOrWhiteSpace(topic))
                throw new ArgumentException("Topic name must be provided.", nameof(topic));

            var body = JsonSerializer.SerializeToUtf8Bytes(message);

            await using var connection = await _factory.CreateConnectionAsync();
            await using var channel = await connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(exchange: topic, type: ExchangeType.Fanout);

            await channel.BasicPublishAsync(
                exchange: topic,
                routingKey: string.Empty,
                body: body);
        }

    }
}