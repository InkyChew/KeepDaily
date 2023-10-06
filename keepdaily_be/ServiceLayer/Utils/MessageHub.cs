using DomainLayer.Models;
using Microsoft.AspNetCore.SignalR;

namespace ServiceLayer.Utils
{
    public class MessageHub : Hub
    {
        public async Task SetUserIdentifier(string userId)
        {
            await Groups.AddToGroupAsync(Context.ConnectionId, userId);
        }
    }
}
