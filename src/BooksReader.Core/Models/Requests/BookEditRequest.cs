using System;

namespace BooksReader.Core.Models.Requests
{
	public class BookEditRequest
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
	}
}
