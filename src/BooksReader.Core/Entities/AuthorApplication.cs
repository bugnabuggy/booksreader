using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using BooksReader.Core.Models;

namespace BooksReader.Core.Entities
{
    public class AuthorApplication: IIdentified
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public DateTimeOffset Created { get; set; }
        public bool Approved { get; set; }

        [ForeignKey("UserId")]
        public BrUser User { get; set; }
    }
}
