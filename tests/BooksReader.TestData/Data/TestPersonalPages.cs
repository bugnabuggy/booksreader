using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Enums;

namespace BooksReader.TestData.Data
{
    public class TestPersonalPages
    {
        public static IEnumerable<PersonalPage> GetPersonalPages()
        {
            return new List<PersonalPage>()
            {
                new PersonalPage()
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                    Content = "<h1>Hello world!</h1>",
                    Domain = "localhost:4200",
                    PageType = PersonalPageType.AuthorPage,
                    SubjectId = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                },

                new PersonalPage()
                {
                    Id = Guid.Parse("20000000-0000-0000-0000-000000000002"),
                    Content = "<h1>Hello world!</h1>",
                    Domain = "localhost:4200",
                    UrlPath = "story",
                    PageType = PersonalPageType.AuthorPage,
                    SubjectId = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                },
            };
        }
    }
}
