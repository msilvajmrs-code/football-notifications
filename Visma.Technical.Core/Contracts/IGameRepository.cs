using System;
using System.Collections.Generic;
using System.Text;
using Visma.Technical.Core.Features.ProcessFootballEvent;

namespace Visma.Technical.Core.Contracts
{
    public enum TeamType
    {
        Home,
        Away
    }
    public class Game
    {
        public int Id { get; set; }
        public required string HomeTeam { get; set; }
        public required string AwayTeam { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }

        public string GetTeam(TeamType teamType)
        {
            return teamType == TeamType.Home ? HomeTeam : AwayTeam;
        }
    }
    public interface IGameRepository
    {
        public Task<Game?> GetGameByIdAsync(int gameId);
    }
}
