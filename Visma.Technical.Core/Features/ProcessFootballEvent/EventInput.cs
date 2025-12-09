using Visma.Technical.Core.Contracts;

namespace Visma.Technical.Core.Features.ProcessFootballEvent
{
    public enum EventType
    {
        Goal,
        YellowCard,
        RedCard,
        Commentary
    }

    public class EventInput
    {
        public TeamType AboutTeam { get; set; }
        public int GameId { get; set; }
        public EventType Type { get; set; }
        public string? Description { get; set; }
    }
}
