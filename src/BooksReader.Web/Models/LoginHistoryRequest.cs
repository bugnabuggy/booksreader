namespace BooksReader.Web.Models
{
	public class LoginHistoryRequest
	{
		public LoginHistoryCoordinates Coordinates { get; set; }
		public LoginHistoryScreen Screen { get; set; }
		public string UserAgent { get; set; }
	}
}