namespace BooksReader.Core.Models.Requests
{
    public class ResetPasswordRequest
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string AntiforgeryKey { get; set; }
    }
}
