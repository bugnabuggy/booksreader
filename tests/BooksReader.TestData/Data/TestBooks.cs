using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;

namespace BooksReader.TestData
{
	public class TestBooks
	{
		public static IEnumerable<Book> GetBooks()
		{
			return new List<Book>()
			{
				new Book()
				{
					OwnerId = Guid.Parse("00000000-0000-0000-0000-00000000000A"),
					Id = Guid.Empty,
					Author = "William Shakespeare",
					Title = "Romeo and Juliet"
				},
                new Book()
                {
                    Id = Guid.Parse("B0000000-0000-0000-0000-000000000001"),
                    Title = "Tony's book",
                    OwnerId = Guid.Parse("00000000-0000-0000-0000-000000000001")
                },
                new Book()
				{
					Id = Guid.Parse("2325a096-1edc-4015-986b-111111111111"),
					Title = "Test book",
					OwnerId = Guid.Parse("00000000-0000-0000-0000-000000000001")
				},
                new Book()
                {
                    Id = Guid.Parse("2325a096-1edc-4015-986b-111111111112"),
                    Title = "Book for deletion",
                    OwnerId = Guid.Parse("00000000-0000-0000-0000-000000000001")
                }
            };
		}

        public static  Book GetBook()
        {
            return GetBooks().ToArray()[1];
        }

    }
}
