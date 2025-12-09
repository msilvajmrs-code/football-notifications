using System;
using System.Collections.Generic;
using System.Text;
using Visma.Technical.Core.Contracts;
using Visma.Technical.Core.Features.ProcessFootballEvent;

namespace Visma.Technical.Core.Features.PublishFootballEvent.InputHandlers
{
    public class GeneralInputHandler : IInputHandler
    {
        public Notification HandleInput(EventInput eventInput, Game game)
        {

            return new Notification
            {
                GameDescription = $"Game: {game.HomeTeam} vs {game.AwayTeam}",
                Score = $"{game.HomeTeamScore}:{game.AwayTeamScore}",
                Message = $"Commentary: {eventInput.Description ?? "No additional details."}"
            };
        }

    }
}
