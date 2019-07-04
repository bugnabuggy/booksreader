using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.Requests
{
    public class UserProfileRequest
    {
        public Guid? Id { get; set; }
        public string Username { get; set; }
        public string Email { get; set; }
        public string Avatar { get; set; }
        public string Name { get; set; }
    }
}
