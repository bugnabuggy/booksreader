using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.DTO.Reader;
using BooksReader.Core.Services.Reader;
using BooksReader.Dictionaries.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.SignalR;

namespace BooksReader.Web.Hubs
{
	[Authorize()]
	public class BooksHub : Hub
	{
		private readonly UserManager<BrUser> _userManager;
        private readonly IBookReadingService _readingSvc;
        private static readonly Dictionary<string, string> _connections = new Dictionary<string, string>();

		public BooksHub(
            UserManager<BrUser> userManager,
            IBookReadingService readingSvc
            )
		{
			this._userManager = userManager;
            this._readingSvc = readingSvc;
        }

		public override async Task OnConnectedAsync()
		{
			var user = await _userManager.GetUserAsync(Context.User);

			var existing = _connections.ContainsValue(user.UserName);
			if (existing)
			{
				var sessionId = _connections.FirstOrDefault(x => x.Value.Equals(user.UserName)).Key;
				_connections.Remove(sessionId);
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

        public async Task<IOperationResult<BookReadingDto>> GetBook(string bookId)
        {
            IOperationResult<BookReadingDto> result = new OperationResult<BookReadingDto>();
            var user = await _userManager.GetUserAsync(Context.User);

            if (Guid.TryParse(bookId, out var bookGuid))
            {
                result = _readingSvc.GetBookForReading(bookGuid, Context.ConnectionId, user);
            }
            else
            {
                result.Messages.Add(MessageStrings.BookReadingMessages.WrongBookId);
            }

            return result;
        }

        public async Task<IOperationResult<BookChapter>> GetChapter(string bookId, string chapterId)
        {
            IOperationResult<BookChapter> result = new OperationResult<BookChapter>();
            var user = await _userManager.GetUserAsync(Context.User);

            if (Guid.TryParse(bookId, out var bookGuid) && (Guid.TryParse(chapterId, out var chapterGuid)))
            {
                result = _readingSvc.GetChapterContent(bookGuid, chapterGuid, user);
            }
            else
            {
                result.Messages.Add(MessageStrings.BookReadingMessages.WrongBookOrChapterId);
            }

            return result;
        }
    }
}
