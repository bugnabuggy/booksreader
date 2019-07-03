using BooksReader.Web.Models;

namespace BooksReader.Core.Models.Requests
{
	public class LoginHistoryRequest
	{
		public LoginHistoryCoordinates Coordinates { get; set; }
		public LoginHistoryScreen Screen { get; set; }
		public string UserAgent { get; set; }
	}
}