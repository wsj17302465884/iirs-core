using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNetCore.SignalR;
using Microsoft.Extensions.Logging;

namespace IIRS.Utilities.SignalRHelper
{
    /// <summary>
    /// TODO 消息Hub 未完成
    /// </summary>
    public class MessageHub : Hub
    {
        /// <summary>
        /// 
        /// </summary>
        //private static List<string> Users = new List<string>();
        //private readonly ILogger<MessageHub> _logger;

        public MessageHub()
        {
            
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="user"></param>
        /// <param name="message"></param>
        /// <returns></returns>
        public async Task SendMessage(string user, string message)
        {
            await Clients.All.SendAsync("ReceiveMessage", user, message);
        }

        public override async Task OnConnectedAsync()
        {
            var claims = Context.User.Claims.Where(x => x.Type == ClaimTypes.Role).ToList();
            for (int i = claims.Count - 1; i >= 0; i--)
            {
                await Groups.AddToGroupAsync(Context.ConnectionId, claims[i].Value);
            }
            await base.OnConnectedAsync();
        }

        public override async Task OnDisconnectedAsync(Exception exception)
        {
            var claims = Context.User.Claims.Where(x => x.Type == ClaimTypes.Role).ToList();
            for (int i = claims.Count - 1; i >= 0; i--)
            {
                await Groups.RemoveFromGroupAsync(Context.ConnectionId, claims[i].Value);
            }
            await base.OnDisconnectedAsync(exception);
        }
    }
}
