// Simplest example without DI using the subscriber

using Visma.Technical.Core.Features.PublishFootballEvent;
using Visma.Technical.Core.Infrastructure.Messaging;

var subscriber = new RabbitMqSubscriber("amqp://guest:guest@localhost:5672/");


await subscriber.SubscribeAsync<Notification>("football_events", notification =>
{
    ArgumentNullException.ThrowIfNull(notification);
    Console.WriteLine($"{notification.GameDescription} [{notification.Score}] - {notification.Message}");
    return Task.CompletedTask;
});
Console.WriteLine("Press any key to stop listening");
Console.ReadKey();