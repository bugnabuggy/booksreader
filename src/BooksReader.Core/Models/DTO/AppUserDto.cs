using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.DTO
{
    public class AppUserDto
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string Avatar { get; set; }
        public string Email { get; set; }
        public string Username { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }
}
