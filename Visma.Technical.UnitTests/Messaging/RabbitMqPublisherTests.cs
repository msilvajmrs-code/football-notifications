using FluentAssertions;
using Visma.Technical.Core.Infrastructure.Messaging;
using Xunit;

namespace Visma.Technical.UnitTests.Messaging
{
    public class RabbitMqPublisherTests
    {
        [Theory]
        [InlineData(null)]
        [InlineData("")]
        [InlineData("   ")]
        public async Task PublishAsync_ThrowsArgumentException_WhenTopicIsNullOrWhitespace(string? topic)
        {
            // Arrange
            var config = new RabbitMqConfiguration
            {
                HostName = "localhost",
                UserName = "_",
                Password = "_"
            };
            var publisher = new RabbitMqPublisher(config);

            // Act
            Func<Task> act = async () => await publisher.PublishAsync(topic!, new { });

            // Assert
            var assertion = await act.Should().ThrowAsync<ArgumentException>();
            assertion.Which.ParamName.Should().Be("topic");
        }
    }
}
