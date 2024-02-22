using InteractiveTyingGameBlazor.DbModels;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;

namespace InteractiveTyingGameBlazor.Models
{
    public class GameSession
    {
        public event Action? OnMatchFinished;
        public Dictionary<string, Game> PlayerGameStates { get; private set; } = [];
        public double Seed { get; private set; }
        public GameSession(double seed, params string[] playerIds)
        {
            foreach (var playerId in playerIds)
            {
                PlayerGameStates[playerId] = new Game(); 
            }
            Seed = seed;
        }

        public void SetPlayerReady(string playerId, bool ready)
        {
            if (PlayerGameStates.TryGetValue(playerId, out Game? value))
            {
                value.Ready = ready;
            }
        }

        private void EvaluatePlayers()
        {
            if (PlayerGameStates.Count > 1) {
                int place = 1;
                foreach (var game in PlayerGameStates.OrderByDescending(i => i.Value.CPM).Select(i => i.Value))
                {
                    game.Placement = place;
                    ++place;
                }
            }
        }

        public void PlayerFinished(string playerId) 
        {
            if (PlayerGameStates.TryGetValue(playerId, out Game? value))
            {   
                value.Finished = true;
                CheckMatchFnished();
            }
        }

        public void CheckMatchFnished()
        {
            if (PlayerGameStates.Values.All(i => i.Finished))
            {   
                EvaluatePlayers();
                OnMatchFinished?.Invoke();
            }
        }

        public void UnregisterPlayer(string playerId)
            => PlayerGameStates.Remove(playerId);        

        public int PlayerCount()
            => PlayerGameStates.Count;

        public bool PlayerExists(string playerId)
            => PlayerGameStates.ContainsKey(playerId);

        public bool IsPlayerReady(string playerId)        
            => PlayerGameStates.TryGetValue(playerId, out var game) && game.Ready;

        public bool MatchReady()
            => PlayerGameStates.Values.All(i => i.Ready);
    }
}