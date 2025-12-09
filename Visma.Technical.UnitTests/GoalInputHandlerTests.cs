using FluentAssertions;
using Visma.Technical.Core.Contracts;
using Visma.Technical.Core.Features.ProcessFootballEvent;
using Visma.Technical.Core.Features.PublishFootballEvent.InputHandlers;
using Xunit;

namespace Visma.Technical.UnitTests
{
    public class GoalInputHandlerTests
    {
        [Fact]
        public void HandleInput_IncrementsHomeScore_AndReturnsNotification_WithDescription()
        {
            // Arrange
            var game = new Game
            {
                Id = 1,
                HomeTeam = "HomeUnited",
                AwayTeam = "AwayCity",
                HomeTeamScore = 0,
                AwayTeamScore = 0
            };

            var input = new EventInput
            {
                AboutTeam = TeamType.Home,
                GameId = game.Id,
                Type = EventType.Goal,
                Description = "Stunning volley"
            };

            var handler = new GoalInputHandler();

            // Act
            var notification = handler.HandleInput(input, game);

            // Assert
            game.HomeTeamScore.Should().Be(1);
            game.AwayTeamScore.Should().Be(0);

            notification.Should().NotBeNull();
            notification.GameDescription.Should().Be($"Game: {game.HomeTeam} vs {game.AwayTeam}");
            notification.Score.Should().Be("1:0");
            notification.Message.Should().Contain(game.HomeTeam);
            notification.Message.Should().Contain("Stunning volley");
        }

        [Fact]
        public void HandleInput_IncrementsAwayScore_AndReturnsNotification_WhenNoDescription_UsesDefaultText()
        {
            // Arrange
            var game = new Game
            {
                Id = 2,
                HomeTeam = "HomeCity",
                AwayTeam = "AwayUnited",
                HomeTeamScore = 0,
                AwayTeamScore = 0
            };

            var input = new EventInput
            {
                AboutTeam = TeamType.Away,
                GameId = game.Id,
                Type = EventType.Goal,
                Description = null
            };

            var handler = new GoalInputHandler();

            // Act
            var notification = handler.HandleInput(input, game);

            // Assert
            game.HomeTeamScore.Should().Be(0);
            game.AwayTeamScore.Should().Be(1);

            notification.Should().NotBeNull();
            notification.GameDescription.Should().Be($"Game: {game.HomeTeam} vs {game.AwayTeam}");
            notification.Score.Should().Be("0:1");
            notification.Message.Should().Contain(game.AwayTeam);
            notification.Message.Should().Contain("No additional details.");
        }
    }
}
