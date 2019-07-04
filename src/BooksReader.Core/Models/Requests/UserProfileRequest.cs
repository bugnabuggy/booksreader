using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.Requests
{
    public class UserProfileRequest
    {
        public string Username { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
    }
}
