using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace BooksReader.Hubs
{
	public class UserHub : Hub
	{
	    private readonly UserManager<TMUser> _userManager;
	    private readonly Dictionary<string, string> _connections;

        public UserHub(UserManager<TMUser> userManager
	        )
        {
            this._userManager = userManager;
            this._connections = new Dictionary<string, string>();
        }

	    public override async Task OnConnectedAsync()
	    {
	        var user = await _userManager.GetUserAsync(Context.User);
            this._connections.Add(user == null ? "none" : user.Id, Context.ConnectionId);

	        await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
	        await base.OnConnectedAsync();
	    }

        public async Task CheckStatistics(string val)
		{
		    var statistics = "val " + Context.Items.Keys.Count;
                                  
			await Clients.All.SendAsync("GetStats", statistics);
		}
	}
}
