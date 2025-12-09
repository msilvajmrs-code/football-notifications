using System;
using System.Collections.Generic;
using System.Text;

namespace Visma.Technical.Core.Features.ProcessFootballEvent
{
    public enum EventType
    {
        Goal,
        YellowCard,
        RedCard,
        Substitution,
        Commentary
    }

    public class EventInput
    {
        public int AboutTeamId { get; set; }
        public int GameId { get; set; }
        public EventType Type { get; set; }
        public string? Description { get; set; }
    }
}
