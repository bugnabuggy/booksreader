using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Enums;

namespace BooksReader.Core.Models.DTO.Admin
{
    public class UserDomainStateDto
    {
        public Guid DomainId { get; set; }
        public string DomainName { get; set; }
        public string Username { get; set; }
        public bool Verified { get; set; }

        public string Protocol { get; set; }
        public DomainVerificationType Type { get; set; }
        public DateTimeOffset? VerificationDate { get; set; }
        public DateTimeOffset? VerificationRequested { get; set; }

        public int NumberOfPages { get; set; }
    }
}
