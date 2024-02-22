namespace InteractiveTyingGameBlazor.MatchMaking
{
    public class MatchmakingMediator
    {
        private readonly Dictionary<string, Func<Task>> _callbacks = [];

        public void Subscribe(string userId, Func<Task> callback)
            => _callbacks[userId] = callback;
        
        public void Unsubscribe(string userId)
            => _callbacks.Remove(userId);
        

        public async Task NotifyMatchFound(string player1, string player2)
        {
            if (_callbacks.TryGetValue(player1, out Func<Task>? value))
            {
                await value();
            }

            if (_callbacks.TryGetValue(player2, out Func<Task>? value2))
            {
                await value2();
            }
        }
    }
}
