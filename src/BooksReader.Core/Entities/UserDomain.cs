using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using BooksReader.Core.Enums;
using BooksReader.Core.Models;

namespace BooksReader.Core.Entities
{
    public class UserDomain: IIdentified, IOwned
    {
        public Guid Id { get; set; }
        [MaxLength(256)]
        public string Name { get; set; }
        public bool Verified { get; set; }

        public Guid OwnerId { get; set; }

        public string Protocol { get; set; }
        public string Certificate { get; set; }

        public Guid VerificationCode { get; set; }
        public DateTime? VerificationDate { get; set; }
        public DateTime? VerificationRequested { get; set; }
        public DomainVerificationType VerificationType { get; set; }

        [ForeignKey("OwnerId")]
        public BrUser User { get; set; }
    }
}
