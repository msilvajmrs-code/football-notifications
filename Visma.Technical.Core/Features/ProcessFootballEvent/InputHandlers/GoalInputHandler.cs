using System;
using System.Collections.Generic;
using System.Text;
using Visma.Technical.Core.Contracts;
using Visma.Technical.Core.Features.ProcessFootballEvent;

namespace Visma.Technical.Core.Features.PublishFootballEvent.InputHandlers
{
    public class GoalInputHandler : IInputHandler
    {
        public Notification HandleInput(EventInput eventInput, Game game)
        {
            _ = eventInput.AboutTeam == TeamType.Home ?
                game.HomeTeamScore++ :
                game.AwayTeamScore++;
            var team = game.GetTeam(eventInput.AboutTeam);
            return new Notification
            {
                GameDescription = $"Game: {game.HomeTeam} vs {game.AwayTeam}",
                Score = $"{game.HomeTeamScore}:{game.AwayTeamScore}",
                Message = $"Goaaaaaaaal!!! {team} scored! Details: {eventInput.Description ?? "No additional details."}"
            };
        }
    }
}
