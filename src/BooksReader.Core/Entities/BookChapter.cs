using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Models;

namespace BooksReader.Core.Entities
{
	public class BookChapter : IIdentified , IOwned
	{
		public Guid Id { get; set; }
		public Guid OwnerId { get; set; }
		public Guid BookId { get; set; }
		public int Number { get; set; }
		public string Title { get; set; }
		public string Content { get; set; }
		public DateTime Created { get; set; }
	}
}
