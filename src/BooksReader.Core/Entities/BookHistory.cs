using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BooksReader.Core.Models;

namespace BooksReader.Core.Entities
{
    public class BookHistory : IIdentified, IOwned
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }

        [MaxLength(1000)]
        public string Title { get; set; }
        [MaxLength(1000)]
        public string Author { get; set; }
        public string Picture { get; set; }

        [MaxLength(3000)]
        public string Description { get; set; }

        public uint Version { get; set; }
        public DateTimeOffset Created { get; set; }
    }
}
