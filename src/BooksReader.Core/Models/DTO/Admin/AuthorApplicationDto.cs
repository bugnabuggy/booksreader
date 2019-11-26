using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.DTO.Admin
{
    public class AuthorApplicationDto
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTimeOffset Created { get; set; }
        public bool Approved { get; set; }

        public string UserName { get; set; }
        public DateTimeOffset UserRegistrationDate { get; set; }
    }
}
