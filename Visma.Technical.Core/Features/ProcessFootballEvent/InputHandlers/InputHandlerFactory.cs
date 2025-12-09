using System;
using System.Collections.Generic;
using System.Numerics;
using System.Text;
using Visma.Technical.Core.Features.PublishFootballEvent.InputHandlers;

namespace Visma.Technical.Core.Features.ProcessFootballEvent.InputHandlers
{
    public class InputHandlerFactory
    {
        public Dictionary<EventType, IInputHandler> Handlers { get; } = new()
        {
            { EventType.Goal, new GoalInputHandler() },
            { EventType.YellowCard, new CardInputHandler() },
            { EventType.RedCard, new CardInputHandler() },
            { EventType.Commentary, new GeneralHandler() }
        };

        public IInputHandler? GetHandler(EventType eventType)
        {
            Handlers.TryGetValue(eventType, out var handler);
            return handler;
        }
    }
}
