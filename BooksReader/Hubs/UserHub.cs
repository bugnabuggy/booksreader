using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace BooksReader.Hubs
{
    [Authorize]
	public class UserHub : Hub
	{
	    private readonly UserManager<TMUser> _userManager;
	    private static readonly Dictionary<string, string> _connections = new Dictionary<string, string>();

        public UserHub(UserManager<TMUser> userManager)
        {
            this._userManager = userManager;
        }

	    public override async Task OnConnectedAsync()
	    {
	        var user = await _userManager.GetUserAsync(Context.User);

	        var existing = _connections.ContainsKey(user.UserName);
	        if (existing)
	        {
	            await this.Logout(_connections[user.UserName]);
	            _connections.Remove(user.UserName);
            }

            _connections.Add(user.UserName, Context.ConnectionId);
            
	        await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
	        await base.OnConnectedAsync();
	    }

        public async Task CheckStatistics(string val)
		{
		    var statistics = $"Connections count [{_connections.Count}]";
                                  
			await Clients.All.SendAsync("GetStats", statistics);
		}

	    public async Task Logout(string sessionId)
	    {
	        await Clients.Client(sessionId).SendAsync("Logout", "deviceinfo");
	    }
    }
}
