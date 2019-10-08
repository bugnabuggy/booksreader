using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.Requests
{
    public class RegistrationRequest
    {
        public string Username { get; set; }
        public string Fullname { get; set; }
        public string Password { get; set; }
        public string AntiforgeryKey { get; set; }
    }
}
