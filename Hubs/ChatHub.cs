using Microsoft.AspNetCore.SignalR;
using Syncfusion.Blazor.Charts.Chart.Internal;

namespace InteractiveTyingGameBlazor.Hubs
{
    public class ChatHub : Hub
    {
        public async Task SendMessage(string senderId, string recipientId, string message)
        {
            await Clients.User(recipientId).SendAsync("ReceiveMessage", senderId, message);
        }
    }
}
