using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Enums;

namespace BooksReader.Core.Models.Requests
{
    public class UserDomainRequest : IIdentified, IOwned
    {
        public Guid Id { get; set; }

        public string Name { get; set; }
        public string Protocol { get; set; }
        public DomainVerificationType VerificationType { get; set; }
        public string Certificate { get; set; }

        public Guid OwnerId { get; set; }
    }
}
