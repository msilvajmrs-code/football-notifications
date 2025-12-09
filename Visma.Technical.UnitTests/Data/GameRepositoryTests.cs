using System.Threading.Tasks;
using FluentAssertions;
using Visma.Technical.Core.Contracts;
using Visma.Technical.Core.Infrastructure.Data;
using Xunit;

namespace Visma.Technical.UnitTests.Data
{
    public class GameRepositoryTests
    {
        [Fact]
        public async Task GetGameByIdAsync_ReturnsGame_WhenGameExists()
        {
            // Arrange
            var repo = new FakeGameRepository();

            // Act
            var game = await repo.GetGameByIdAsync(1);

            // Assert
            game.Should().NotBeNull();
            game!.Id.Should().Be(1);
            game.HomeTeam.Should().Be("SLBenfica");
            game.AwayTeam.Should().Be("Viking");
        }

        [Fact]
        public async Task GetGameByIdAsync_ReturnsNull_WhenGameDoesNotExist()
        {
            // Arrange
            var repo = new FakeGameRepository();

            // Act
            var game = await repo.GetGameByIdAsync(9999);

            // Assert
            game.Should().BeNull();
        }

        [Theory]
        [InlineData(2, "Molde", "FCPorto")]
        [InlineData(3, "Rosenborg", "SportingCP")]
        public async Task GetGameByIdAsync_ReturnsExpectedGame_ForMultipleIds(int id, string expectedHome, string expectedAway)
        {
            // Arrange
            var repo = new FakeGameRepository();

            // Act
            var game = await repo.GetGameByIdAsync(id);

            // Assert
            game.Should().NotBeNull();
            game!.Id.Should().Be(id);
            game.HomeTeam.Should().Be(expectedHome);
            game.AwayTeam.Should().Be(expectedAway);
        }
    }
}