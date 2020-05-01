using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.SignalR;

namespace PWAppApi.Services
{
    [Authorize]
    public class SignalrHub: Hub
    {
        public async Task SendToUser(string user, string message)
        {
            await Clients.User(user).SendAsync("ReceiveDirectMessage", $"{Context.UserIdentifier}: {message}");
        }

        public async Task Send(string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", $"{Context.UserIdentifier}: {message}");
        }
 
        public override async Task OnConnectedAsync()
        {
            await Clients.All.SendAsync("ReceiveMessage", $"User {Context.UserIdentifier} joined.");
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            await Clients.All.SendAsync("ReceiveMessage", $"User {Context.UserIdentifier} left.");
            await base.OnDisconnectedAsync(exception);
        }
    }
}
