using InteractiveTyingGameBlazor.Models;

namespace InteractiveTyingGameBlazor.Chat
{
    public class PublicChatService
    {  
        public List<PublicMessage> Messages { get; } = [];
        public delegate Task AsyncEventHandler();

        public event AsyncEventHandler? UpdateAsync;

        private const int MESSAGE_LIMIT = 20;
        public async Task UpdateChatAsync()
        {
            var handlers = UpdateAsync?.GetInvocationList().Cast<AsyncEventHandler>();
            if (handlers != null)
            {
                await Task.WhenAll(handlers.Select(handler => handler()));
            }
        }

        public void AddMessage(PublicMessage message)
        {
            Messages.Add(message);

            if (Messages.Count > MESSAGE_LIMIT)
            {
                Messages.RemoveAt(0);
            }
        }
    }
}
