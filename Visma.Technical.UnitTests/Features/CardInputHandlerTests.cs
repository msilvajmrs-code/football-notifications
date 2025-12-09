using FluentAssertions;
using Visma.Technical.Core.Contracts;
using Visma.Technical.Core.Features.ProcessFootballEvent;
using Visma.Technical.Core.Features.PublishFootballEvent.InputHandlers;
using Xunit;

namespace Visma.Technical.UnitTests.Features
{
    public class CardInputHandlerTests
    {
        [Fact]
        public void HandleInput_ReturnsNotification_WithTeamAndDescription_ForHomeTeam()
        {
            // Arrange
            var game = new Game
            {
                Id = 1,
                HomeTeam = "HomeRovers",
                AwayTeam = "AwayRovers",
                HomeTeamScore = 0,
                AwayTeamScore = 0
            };

            var input = new EventInput
            {
                AboutTeam = TeamType.Home,
                GameId = game.Id,
                Type = EventType.YellowCard,
                Description = "Late tackle"
            };

            var handler = new CardInputHandler();

            // Act
            var notification = handler.HandleInput(input, game);

            // Assert
            notification.Should().NotBeNull();
            notification.GameDescription.Should().Be($"Game: {game.HomeTeam} vs {game.AwayTeam}");
            notification.Score.Should().Be("0:0");
            notification.Message.Should().Contain(input.Type.ToString());
            notification.Message.Should().Contain(game.HomeTeam);
            notification.Message.Should().Contain("Late tackle");
        }

        [Fact]
        public void HandleInput_ReturnsNotification_WithDefaultDetails_WhenDescriptionIsNull_ForAwayTeam()
        {
            // Arrange
            var game = new Game
            {
                Id = 2,
                HomeTeam = "City FC",
                AwayTeam = "United FC",
                HomeTeamScore = 2,
                AwayTeamScore = 1
            };

            var input = new EventInput
            {
                AboutTeam = TeamType.Away,
                GameId = game.Id,
                Type = EventType.RedCard,
                Description = null
            };

            var handler = new CardInputHandler();

            // Act
            var notification = handler.HandleInput(input, game);

            // Assert
            notification.Should().NotBeNull();
            notification.GameDescription.Should().Be($"Game: {game.HomeTeam} vs {game.AwayTeam}");
            notification.Score.Should().Be("2:1");
            notification.Message.Should().Contain(input.Type.ToString());
            notification.Message.Should().Contain(game.AwayTeam);
            notification.Message.Should().Contain("No additional details.");
        }
    }
}