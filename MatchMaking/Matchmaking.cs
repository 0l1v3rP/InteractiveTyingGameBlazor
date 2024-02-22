using InteractiveTyingGameBlazor.Models;
using Microsoft.AspNetCore.SignalR;

namespace InteractiveTyingGameBlazor.MatchMaking
{
    public class MatchmakingService(MatchmakingMediator mediator)
    {
        private readonly MatchmakingMediator _mediator = mediator;
        private readonly List<string> _playersSearching = [];
        private readonly List<GameSession> _sessions = [];
        private readonly Random _random = new();
        public bool AddPlayerToQueue(string userId)
        {
            if (!_playersSearching.Contains(userId))
            {
                _playersSearching.Add(userId);
                return true;
            }
            return false;
        }

        public async Task TryMatchPlayers()
        {
            while (_playersSearching.Count >= 2)
            {
                var player1 = _playersSearching[0];
                var player2 = _playersSearching[1];
                _playersSearching.RemoveRange(0, 2);
                await NotifyMatchFound(player1, player2);
            }
        }

        private async Task NotifyMatchFound(string player1, string player2)
        {
            var gameSession = new GameSession(_random.NextDouble() ,player1, player2);
            _sessions.Add(gameSession);
            await _mediator.NotifyMatchFound(player1, player2);
        }

        public GameSession? GetSession(string playerId)
            => _sessions.FirstOrDefault(i => i.PlayerExists(playerId));
        
        public void SetPlayerReady(string playerId, bool ready)
            => GetSession(playerId)?.SetPlayerReady(playerId, ready);

        public bool MatchReady(string playerId)
            => GetSession(playerId).MatchReady();


        public double GetSeedValue(string playerId)
            => GetSession(playerId).Seed;

        public void TryCreateSession(string playerId)
        {
            if (GetSession(playerId) is null)
            {
                _sessions.Add(new GameSession(_random.NextDouble(), playerId));
            }
        }
        public void UnregisterFromSession(string playerId)
        {
            if (_sessions.RemoveAll(i => i.PlayerExists(playerId) && i.PlayerCount() == 1) == 0)
                GetSession(playerId).UnregisterPlayer(playerId);
        } 
    }
}
