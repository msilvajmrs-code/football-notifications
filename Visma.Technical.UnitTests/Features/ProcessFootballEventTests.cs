using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using NSubstitute;
using System;
using System.Threading.Tasks;
using Visma.Technical.Core.Contracts;
using Visma.Technical.Core.Features.ProcessFootballEvent;
using Visma.Technical.Core.Features.ProcessFootballEvent.InputHandlers;
using Visma.Technical.Core.Features.PublishFootballEvent;
using Visma.Technical.Core.Features.PublishFootballEvent.InputHandlers;
using Xunit;

namespace Visma.Technical.UnitTests.Features
{
    public class ProcessFootballEventTests
    {
        [Fact]
        public async Task Should_throw_argument_null_exception_When_input_is_null()
        {
            // Arrange
            var repo = Substitute.For<IGameRepository>();
            var mq = Substitute.For<IMqPublisher>();
            var serviceProvider = Substitute.For<IKeyedServiceProvider>();

            var sut = new ProcessFootballEvent(repo, mq, serviceProvider);

            // Act
            Func<Task> act = () => sut.ProcessEventAsync(null!);

            // Assert
            await act.Should().ThrowAsync<ArgumentNullException>();
        }

        [Fact]
        public async Task Should_throw_invalid_operation_When_game_does_not_exist()
        {
            // Arrange
            var gameId = 99;
            var repo = Substitute.For<IGameRepository>();
            repo.GetGameByIdAsync(gameId).Returns(Task.FromResult<Game?>(null));

            var mq = Substitute.For<IMqPublisher>();

            var handler = Substitute.For<IInputHandler>();
            var serviceProvider = Substitute.For<IKeyedServiceProvider>();
            serviceProvider.GetService(Arg.Any<Type>()).Returns(ci => handler);

            var sut = new ProcessFootballEvent(repo, mq, serviceProvider);

            var input = new EventInput
            {
                GameId = gameId,
                Type = EventType.Goal,
                AboutTeam = TeamType.Home,
                Description = "Test"
            };

            // Act
            Func<Task> act = () => sut.ProcessEventAsync(input);

            // Assert
            await act.Should()
                .ThrowAsync<InvalidOperationException>()
                .WithMessage($"Game with ID {gameId} not found.");
        }

        [Fact]
        public async Task Should_publish_notification_for_event_with_input_handler()
        {
            // Arrange
            var gameId = 1;
            var game = new Game { Id = gameId, HomeTeam = "HomeFC", AwayTeam = "AwayFC", HomeTeamScore = 0, AwayTeamScore = 0 };
            var repo = Substitute.For<IGameRepository>();
            repo.GetGameByIdAsync(gameId).Returns(Task.FromResult<Game?>(game));

            var mq = Substitute.For<IMqPublisher>();
            Notification? published = null;
            mq.When(x => x.PublishAsync<Notification>(Arg.Any<string>(), Arg.Any<Notification>()))
              .Do(ci => published = ci.ArgAt<Notification>(1));

            // Provide a concrete input handler substitute that updates score and returns a Notification
            var notification = new Notification
            {
                GameDescription = "Game",
                Score = "1:1",
                Message = "Testing"
            };
            var handler = Substitute.For<IInputHandler>();
            handler.HandleInput(Arg.Any<EventInput>(), Arg.Any<Game>()).Returns(ci =>
            {
                return notification;
            });

            var serviceProvider = Substitute.For<IKeyedServiceProvider>();
            serviceProvider.GetService(Arg.Any<Type>()).Returns(ci => handler);

            var sut = new ProcessFootballEvent(repo, mq, serviceProvider);

            var input = new EventInput
            {
                GameId = gameId,
                Type = EventType.Goal,
                AboutTeam = TeamType.Home
            };

            // Act
            await sut.ProcessEventAsync(input);

            // Assert
            await mq.Received(1).PublishAsync<Notification>("football_events", Arg.Any<Notification>());
        }
    }
}