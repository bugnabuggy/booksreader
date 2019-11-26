using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using BooksReader.Core.Models;

namespace BooksReader.Core.Entities
{
	public class BookChapter : IIdentified , IOwned
	{
		public Guid Id { get; set; }
		public Guid OwnerId { get; set; }
		public Guid BookId { get; set; }
		public uint Number { get; set; }

        [MaxLength(1000)]
		public string Title { get; set; }
        [MaxLength(3000)]
        public string Description { get; set; }

        public string Content { get; set; }
		public DateTimeOffset Created { get; set; }
        public bool IsPublished { get; set; }

        public bool Verified { get; set; }
        public uint Version { get; set; }

        [ForeignKey("BookId")]
        public Book Book { get; set; }
	}
}
