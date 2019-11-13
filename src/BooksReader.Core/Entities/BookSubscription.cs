using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using BooksReader.Core.Models;

namespace BooksReader.Core.Entities
{
    public class BookSubscription: IIdentified
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid BookId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
        public string Comment { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }
    }
}
