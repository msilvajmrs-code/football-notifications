using FluentAssertions;
using Visma.Technical.Core.Infrastructure.Messaging;
using Xunit;

namespace Visma.Technical.UnitTests.Messaging
{
    public class RabbitMqSubscriberTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task SubscribeAsync_ThrowsArgumentException_WhenTopicIsNullOrWhitespace(string? topic)
        {
            // Arrange
            var config = new RabbitMqConfiguration
            {
                HostName = "localhost",
                UserName = "_",
                Password = "_"
            };
            using var subscriber = new RabbitMqSubscriber(config);

            // Act
            Func<Task> act = () => subscriber.SubscribeAsync<object>(topic!, _ => Task.CompletedTask);

            // Assert
            var ex = await act.Should().ThrowAsync<ArgumentException>();
            ex.Which.ParamName.Should().Be("topic");
        }
    }
}
