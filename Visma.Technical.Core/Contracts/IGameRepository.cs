using System;
using System.Collections.Generic;
using System.Text;

namespace Visma.Technical.Core.Contracts
{
    public class Game
    {
        public int Id { get; set; }
        public required string HomeTeam { get; set; }
        public required string AwayTeam { get; set; }
        public int HomeTeamScore { get; set; }
        public int AwayTeamScore { get; set; }
    }
    public interface IGameRepository
    {
        public Task<Game?> GetGameByIdAsync(int gameId);
    }
}
