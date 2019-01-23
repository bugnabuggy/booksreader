using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNet.SignalR;

namespace BooksReader.Hubs
{
	public class UserHub : Hub
	{
		public async Task CheckStatistics()
		{
			await Clients.All.SendAsync("ReceiveMessage", "user", "message");
		}
	}
}
