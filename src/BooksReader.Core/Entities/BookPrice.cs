using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using BooksReader.Core.Models;

namespace BooksReader.Core.Entities
{
    public class BookPrice : IIdentified, IOwned
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }
        public DateTimeOffset Created { get; set; }
        public double Price { get; set; }

        public Guid BookId { get; set; }
        public int CurrencyId { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }

        [ForeignKey("CurrencyId")]
        public TypeValue Currency { get; set; }
    }
}
