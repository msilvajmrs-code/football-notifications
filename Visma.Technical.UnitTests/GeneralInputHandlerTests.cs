using FluentAssertions;
using Visma.Technical.Core.Contracts;
using Visma.Technical.Core.Features.ProcessFootballEvent;
using Visma.Technical.Core.Features.PublishFootballEvent.InputHandlers;
using Xunit;

namespace Visma.Technical.UnitTests
{
    public class GeneralInputHandlerTests
    {
        [Fact]
        public void HandleInput_ReturnsCommentaryNotification_WithProvidedDescription()
        {
            // Arrange
            var game = new Game
            {
                Id = 1,
                HomeTeam = "HomeTown",
                AwayTeam = "AwayTown",
                HomeTeamScore = 2,
                AwayTeamScore = 1
            };

            var input = new EventInput
            {
                AboutTeam = TeamType.Home,
                GameId = game.Id,
                Type = EventType.Commentary,
                Description = "Substitution: Player X in, Player Y out"
            };

            var handler = new GeneralInputHandler();

            // Act
            var notification = handler.HandleInput(input, game);

            // Assert
            notification.Should().NotBeNull();
            notification.GameDescription.Should().Be($"Game: {game.HomeTeam} vs {game.AwayTeam}");
            notification.Score.Should().Be("2:1");
            notification.Message.Should().StartWith("Commentary:");
            notification.Message.Should().Contain("Substitution: Player X in, Player Y out");
        }

        [Fact]
        public void HandleInput_ReturnsCommentaryNotification_WithDefaultDetails_WhenDescriptionIsNull()
        {
            // Arrange
            var game = new Game
            {
                Id = 2,
                HomeTeam = "Alpha FC",
                AwayTeam = "Beta FC",
                HomeTeamScore = 0,
                AwayTeamScore = 0
            };

            var input = new EventInput
            {
                AboutTeam = TeamType.Away,
                GameId = game.Id,
                Type = EventType.Commentary,
                Description = null
            };

            var handler = new GeneralInputHandler();

            // Act
            var notification = handler.HandleInput(input, game);

            // Assert
            notification.Should().NotBeNull();
            notification.GameDescription.Should().Be($"Game: {game.HomeTeam} vs {game.AwayTeam}");
            notification.Score.Should().Be("0:0");
            notification.Message.Should().StartWith("Commentary:");
            notification.Message.Should().Contain("No additional details.");
        }
    }
}