using Microsoft.AspNetCore.SignalR;

namespace SignalR_Chat.Hubs
{
    public class ChatHub: Hub
    {
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.ConnectionId} ozuna yo quiero conocerte");
            await Console.Out.WriteLineAsync($"connected {Context.ConnectionId}");
        }

        public async Task JoinGroup(string groupName, string userName)
        {
            await Groups.AddToGroupAsync($"{Context.ConnectionId}", groupName);
            await Clients.Group(groupName).SendAsync($"the user {userName} join to the party");
        }

        public async Task LeaveGroup(string groupName, string userName)
        {
            await Clients.Groups(groupName).SendAsync($"{userName} leave the party 😭");
            await Groups.RemoveFromGroupAsync(Context.ConnectionId, groupName);

        }

        public async Task SendMessage(string groupName, string userName, string message)
        {
            await Clients.Group(groupName).SendAsync("NewMessage", userName, message);
        }
    }
}
