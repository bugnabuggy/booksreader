using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;

namespace BooksReader.TestData.Data
{
    public class TestBookChapters
    {
        public static IEnumerable<BookChapter> GetChapters()
        {
            return new List<BookChapter>()
            {
                new BookChapter()
                {
                    Id = Guid.Parse("BC000000-0000-0000-0000-000000000001"),
                    OwnerId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    BookId = Guid.Parse("B0000000-0000-0000-0000-000000000001"),
                    Title = "Chapter 1",
                    Content = "<p>No</p>",
                    Number = 0,
                    IsPublished = true,
                    Version = 0,
                    Created = DateTime.Parse("2019-08-23"),
                    Description = "Test chapter 1"
                },
                new BookChapter()
                {
                    Id = Guid.Parse("BC000000-0000-0000-0000-000000000002"),
                    OwnerId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    BookId = Guid.Parse("B0000000-0000-0000-0000-000000000001"),
                    Title = "Chapter 1",
                    Content = "<p>Yes</p>",
                    Number = 1,
                    IsPublished = false,
                    Version = 0,
                    Created = DateTime.Parse("2019-08-24"),
                    Description = "Test chapter 2"
                },
                new BookChapter()
                {
                    Id = Guid.Parse("BC000000-0000-0000-0000-000000000003"),
                    OwnerId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    BookId = Guid.Parse("B0000000-0000-0000-0000-000000000001"),
                    Title = "Chapter 3",
                    Content = "<p>Maybe</p>",
                    Number = 2,
                    IsPublished = true,
                    Version = 0,
                    Created = DateTime.Parse("2019-08-25"),
                    Description = "Test chapter 3"
                },
            };
        }
    }
}
