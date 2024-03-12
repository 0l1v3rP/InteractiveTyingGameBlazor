using InteractiveTyingGameBlazor.DbModels;
using Microsoft.Identity.Client;
using System;
using System.Collections.Generic;

namespace InteractiveTyingGameBlazor.Models
{
    public class GameSession(double seed, int playersNum, GameConfig config) : BaseEntity
    {
        public event Action? OnMatchFinished;
        public event Func<Task>? OnMatchStart;
        public Dictionary<string, Game> PlayerGameStates { get; } = [];
        public double Seed { get; } = seed;
        public int PlayersNum { get; } = playersNum;
        public GameConfig Config { get; } = config;

        public bool Started { get; set; } = false;

        public bool AddPlayer(string playerId)
        {
            if (PlayerGameStates.Count < PlayersNum)
            { 
                PlayerGameStates[playerId] = new Game();
                return true;
            }
            return false;
        }

        public void TryToStartMatch()
        {  
            if(PlayerGameStates.Count == PlayersNum)
            {
                OnMatchStart?.Invoke();
            }
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

        public bool PlayerExists(string playerId)
            => PlayerGameStates.ContainsKey(playerId);

        public bool IsPlayerReady(string playerId)        
            => PlayerGameStates.TryGetValue(playerId, out var game) && game.Ready;

        public bool MatchReady()
            => PlayerGameStates.Values.All(i => i.Ready);
    }
}