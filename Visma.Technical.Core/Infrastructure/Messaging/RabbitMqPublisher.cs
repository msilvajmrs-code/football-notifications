using RabbitMQ.Client;
using System;
using System.Text.Json;
using Visma.Technical.Core.Contracts;

namespace Visma.Technical.Core.Infrastructure.Messaging
{
    public class RabbitMqPublisher : IMqPublisher, IDisposable
    {
        // To make this completely correct, we should ideally abstract connection factory for testability - simplified for the exercise

        private readonly ConnectionFactory _factory;
        private IConnection? _connection;

        public RabbitMqPublisher(RabbitMqConfiguration config)
        {
            _factory = new ConnectionFactory
            {
                Uri = new Uri($"amqp://{config.UserName}:{config.Password}@{config.HostName}")
            };
        }

        public async Task PublishAsync<T>(string topic, T message)
        {
            if (string.IsNullOrWhiteSpace(topic))
                throw new ArgumentException("Topic name must be provided.", nameof(topic));

            var body = JsonSerializer.SerializeToUtf8Bytes(message);

            _connection ??= await _factory.CreateConnectionAsync();
            await using var channel = await _connection.CreateChannelAsync();

            await channel.ExchangeDeclareAsync(exchange: topic, type: ExchangeType.Fanout);

            await channel.BasicPublishAsync(
                exchange: topic,
                routingKey: string.Empty,
                body: body);
        }

        public void Dispose()
        {
            _connection?.Dispose();
        }

    }
}