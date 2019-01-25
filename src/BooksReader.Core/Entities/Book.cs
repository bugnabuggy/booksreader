using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Entities
{
	public class Book
	{
		public Guid Id { get; set; }
		public string Title { get; set; }

		public Guid Autor { get; set; }
		public string AuthorName { get; set; }
	}
}
