using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.SignalR;
using Microsoft.AspNetCore.Authorization;
using InteractiveTyingGameBlazor.Chat;


namespace InteractiveTyingGameBlazor.Hubs
{
    [Authorize]
    public class ChatHub(ConnectionService connectionService, ) : Hub
    {
        private readonly ConnectionService _connectionService = connectionService;

        public async Task SendMessage(string sender, string receiver, string message)
        {
            var connections = _connectionService.GetUserConnections(receiver);

            foreach (var connection in connections)
            {
                await Clients.Client(connection).SendAsync("ReceiveMessage", message);
            }
        }

        public override async Task OnConnectedAsync()
        {
            var user = Context.User.Identity.Name;
            _connectionService.AddConnection(user, Context.ConnectionId);
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {
            var user = Context.User.Identity.Name;
            _connectionService.RemoveConnection(user, Context.ConnectionId);
            await base.OnDisconnectedAsync(exception);
        }
    }
}