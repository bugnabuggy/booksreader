using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;

namespace BooksReader.TestData
{
	class BooksList
	{
		public static IEnumerable<Book> GetBooks()
		{
			return new List<Book>()
			{
				new Book()
				{
					Id = Guid.Empty,
					Author = "William Shakespeare",
					Title = "Romeo and Juliet"
				},
				new Book()
				{
					Id = Guid.Parse("2325a096-1edc-4015-986b-111111111111"),
					Title = "Test book",
					OwnerId = Guid.Parse("00000000-0000-0000-0000-000000000001")
				}
			};
		}
	}
}
