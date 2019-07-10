using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Models.Requests;

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
                    UserId = Guid.Parse("00000000-0000-0000-0000-000000000000"),
                    AuthorName = "Empty Author Pseudonym",
                    Description = "Test empty author"
                },

                new AuthorProfile()
                {
                    Id = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                    UserId = Guid.Parse("00000000-0000-0000-0000-000000000001"),
                    AuthorName = "Tony",
                    Description = "Tony's author page"
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

        public static AuthorProfileRequest GetAuthorProfileEditRequest()
        {
            return new AuthorProfileRequest
            {
                ProfileId = Guid.Parse("10000000-0000-0000-0000-000000000001"),
                AuthorName = "Tony 2",
                Description = "Edited Tony's author page",
                DomainName = "localhost:4200",
                PageContent = "<h1>Hello world!</h1>"
            };
        }

    }
}
