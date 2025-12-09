
using Visma.Technical.Core.Contracts;
using Visma.Technical.Core.Features.ProcessFootballEvent.InputHandlers;

namespace Visma.Technical.Core.Features.ProcessFootballEvent
{
    public class ProcessFootballEvent(
        IGameRepository gameRepository, 
        IMqPublisher mqPublisher,
        InputHandlerFactory handlerFactory) : IProcessFootballEvent
    {

        public async Task ProcessEventAsync(EventInput footballEvent)
        {
            if (footballEvent == null)
            {
                throw new ArgumentNullException(nameof(footballEvent));
            }
            var handler = handlerFactory.GetHandler(footballEvent.Type) ?? 
                throw new InvalidOperationException($"No handler found for event type {footballEvent.Type}.");
            var game = await gameRepository.GetGameByIdAsync(footballEvent.GameId) ?? 
                throw new InvalidOperationException($"Game with ID {footballEvent.GameId} not found.");

            await mqPublisher.PublishAsync("football_events_queue", handler.HandleInput(footballEvent));
        }
    }
}
