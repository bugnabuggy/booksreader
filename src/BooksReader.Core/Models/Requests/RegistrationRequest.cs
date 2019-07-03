namespace BooksReader.Core.Models.Requests
{
	public class RegistrationRequest
	{
		public string AntiforgeryKey { get; set; }
		public string Username { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
	}
}
