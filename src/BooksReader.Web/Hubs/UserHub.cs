using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace BooksReader.Web.Hubs
{
	[Authorize]
	public class UserHub : Hub
	{
		private readonly UserManager<BrUser> _userManager;
		private static readonly Dictionary<string, string> _connections = new Dictionary<string, string>();

		public UserHub(UserManager<BrUser> userManager)
		{
			this._userManager = userManager;
		}

		public override async Task OnConnectedAsync()
		{
			var user = await _userManager.GetUserAsync(Context.User);

			var existing = _connections.ContainsValue(user.UserName);
			if (existing)
			{
				var key = _connections.FirstOrDefault(x => x.Value.Equals(user.UserName)).Key;
				await this.Logout(key);
				_connections.Remove(key);
			}

			_connections.Add(Context.ConnectionId, user.UserName);

			await Groups.AddToGroupAsync(Context.ConnectionId, "SignalR Users");
			await base.OnConnectedAsync();
		}

		public override async Task OnDisconnectedAsync(Exception exception)
		{
			_connections.Remove(Context.ConnectionId);
			await base.OnDisconnectedAsync(exception);
		}

		public async Task CheckStatistics(string val)
		{
			var statistics = $"Connections count [{_connections.Count}]";

			await Clients.All.SendAsync("GetStats", statistics);
		}

		public async Task Logout(string sessionId)
		{

			var info = new
			{
				Ip = Context.GetHttpContext().Connection.RemoteIpAddress.ToString(),
				Browser = Context.GetHttpContext().Request.Headers["User-Agent"].ToString()
			};

			await Clients.Client(sessionId).SendAsync("Logout", info);
		}
	}
}
