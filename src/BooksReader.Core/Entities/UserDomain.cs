using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using BooksReader.Core.Enums;
using BooksReader.Core.Models;

namespace BooksReader.Core.Entities
{
    public class UserDomain: IIdentified
    {
        public Guid Id { get; set; }
        [MaxLength(256)]
        public string Name { get; set; }
        public bool IsVerified { get; set; }

        public Guid UserId { get; set; }

        public Guid VerificationCode { get; set; }
        public DateTime? VerificationDate { get; set; }
        public DateTime? VerificationRequested { get; set; }
        public DomainVerificationType VerificationType{ get; set; }

        [ForeignKey("UserId")]
        public BrUser User { get; set; }
    }
}
