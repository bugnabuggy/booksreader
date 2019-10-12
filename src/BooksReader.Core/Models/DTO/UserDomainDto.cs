using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Enums;

namespace BooksReader.Core.Models.DTO
{
    public class UserDomainDto : IIdentified
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public bool Verified { get; set; }
        public Guid OwnerId { get; set; }

        public string Protocol { get; set; }
        public Guid VerificationCode { get; set; }
        public DomainVerificationType VerificationType { get; set; }
    }
}
