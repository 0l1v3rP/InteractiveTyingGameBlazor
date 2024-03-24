using InteractiveTyingGameBlazor.Models;
using Microsoft.AspNetCore.SignalR;

namespace InteractiveTyingGameBlazor.MatchMaking
{
    public class MatchmakingService()
    {
        private readonly Random _random = new();
        public List<GameSession> Sessions { get; } = [];
        public bool JoinMatch(Guid matchId, string playerId, Func<Task>? onStart)
        {
            if (GetSession(playerId) is not null)
                return false;

            var match = Sessions.First(i => i.Id == matchId);
            bool joined = match.AddPlayer(playerId);
            if (joined)
             {
                match.OnMatchStart += onStart;
                match.TryToStartMatch();
            }
            return joined;
        }
           
        public GameSession? GetSession(string playerId)
            => Sessions.FirstOrDefault(i => i.PlayerExists(playerId));
       
        public Guid? TryCreateSession(string playerId, int playersNum, GameConfig config)
        {
            if (GetSession(playerId) is null)
            {
                Sessions.Add(new GameSession(_random.NextDouble(), playersNum, config));
                return Sessions.Last().Id;
            }
            return null;
        }

        public void UnregisterFromSession(string playerId)
        {
            if (Sessions.RemoveAll(i => i.PlayerExists(playerId) && i.PlayerGameStates.Count == 1) == 0)
                GetSession(playerId)?.UnregisterPlayer(playerId);
        }
    }
}
