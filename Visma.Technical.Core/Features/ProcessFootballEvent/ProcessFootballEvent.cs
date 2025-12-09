
using Microsoft.Extensions.DependencyInjection;
using Visma.Technical.Core.Contracts;
using Visma.Technical.Core.Features.PublishFootballEvent.InputHandlers;

namespace Visma.Technical.Core.Features.ProcessFootballEvent
{


    public class ProcessFootballEvent(
        IGameRepository gameRepository, 
        IMqPublisher mqPublisher,
        IKeyedServiceProvider serviceProvider) : IProcessFootballEvent
    {

        public async Task ProcessEventAsync(EventInput footballEvent)
        {
            ArgumentNullException.ThrowIfNull(footballEvent);
            var handler = serviceProvider.GetRequiredKeyedService<IInputHandler>(footballEvent.Type.ToString());
            var game = await gameRepository.GetGameByIdAsync(footballEvent.GameId) ?? 
                throw new InvalidOperationException($"Game with ID {footballEvent.GameId} not found.");

            await mqPublisher.PublishAsync("football_events", handler?.HandleInput(footballEvent, game));
        }
    }
}
