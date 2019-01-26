using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.DTO
{
    public class UserResult
    {
        public string UserName { get; set; }
        public string Name { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }

}
