using Visma.Technical.Core.Features.ProcessFootballEvent;

namespace Visma.Technical.Core.Features
{
    public interface IProcessFootballEvent
    {
        Task ProcessEventAsync(EventInput footballEvent);
    }
}