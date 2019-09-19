using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Models.DTO;

namespace BooksReader.TestData.Data
{
    public class TestAuthors
    {
        public static IEnumerable<AuthorProfile> GetAuthorProfiles()
        {
            return new List<AuthorProfile>()
            {
                new AuthorProfile()
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000000"),
                    UserId = Guid.Parse("00000000-0000-0000-0000-00000000000A"),
                    AuthorName = "Empty Author Pseudonym",
                    Description = "Test empty author"
                },

                new AuthorProfile()
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                    UserId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    AuthorName = "Tony",
                    Description = "Tony's author page",
                    PersonalPageId = Guid.Parse("20000000-0000-0000-0000-000000000001"),
                },

                new AuthorProfile()
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000002"),
                    UserId = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                    AuthorName = "test",
                    Description = "test author without personal page ",
                }
            };
        }

        public static AuthorProfile GetAuthorProfile()
        {
            return new AuthorProfile()
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                UserId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                AuthorName = "Tony",
                Description = "Tony's author page"
            };
        }

        public static AuthorProfileDto GetAuthorProfileEditRequest()
        {
            return new AuthorProfileDto
            {
                Id = Guid.Parse("10000000-0000-0000-0000-000000000002"),
                AuthorName = "test Edit 2",
                Description = "Edited test author page",
                DomainName = "localhost:4201",
                PageContent = "<h1>Hello world!</h1>"
            };
        }

    }
}
