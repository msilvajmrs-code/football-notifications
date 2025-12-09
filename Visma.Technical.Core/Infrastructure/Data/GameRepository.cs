using System;
using System.Collections.Generic;
using System.Text;
using Visma.Technical.Core.Contracts;

namespace Visma.Technical.Core.Infrastructure.Data
{
    public class FakeGameRepository : IGameRepository
    {
        private readonly Dictionary<int, Game> _games = new()
        {
            { 1, new Game { Id = 1, HomeTeam = "SLBenfica", AwayTeam = "Viking"} },
            { 2, new Game { Id = 2, HomeTeam = "Molde", AwayTeam = "FCPorto"} },
            { 3, new Game { Id = 3, HomeTeam = "Rosenborg", AwayTeam = "SportingCP"} }
        };
        public Task<Game?> GetGameByIdAsync(int gameId)
        {
            return Task.FromResult(_games.GetValueOrDefault(gameId));
        }
    }
}
