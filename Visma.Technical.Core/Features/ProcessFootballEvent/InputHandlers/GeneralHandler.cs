using System;
using System.Collections.Generic;
using System.Text;
using Visma.Technical.Core.Features.ProcessFootballEvent;

namespace Visma.Technical.Core.Features.PublishFootballEvent.InputHandlers
{
    public class GeneralHandler : IInputHandler
    {
        public Notification HandleInput(EventInput eventInput)
        {
            return new Notification
            {
                GameDescription = $"Game ID: {eventInput.GameId}",
                Score = "N/A",
                Message = $"{eventInput.Type} issued to team ID {eventInput.AboutTeamId}. Details: {eventInput.Description ?? "No additional details."}"
            };
        }

    }
}
