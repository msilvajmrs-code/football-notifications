// Simplest example without DI using the subscriber

using Visma.Technical.Core.Features.PublishFootballEvent;
using Visma.Technical.Core.Infrastructure.Messaging;

var rabbitMqConfig = new RabbitMqConfiguration
{
    HostName = "localhost:5672",
    UserName = "guest",
    Password = "guest" // Note: In production, use secure methods to handle sensitive information - this is just for local
};
var subscriber = new RabbitMqSubscriber(rabbitMqConfig);


await subscriber.SubscribeAsync<Notification>("football_events", notification =>
{
    ArgumentNullException.ThrowIfNull(notification);
    Console.WriteLine($"{notification.GameDescription} [{notification.Score}]");
    return Task.CompletedTask;
});
Console.WriteLine("Press any key to stop listening");
Console.ReadKey();