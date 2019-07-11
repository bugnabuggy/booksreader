using System;
using System.Collections.Generic;
using BooksReader.Core.Entities;

namespace BooksReader.TestData.Data
{
    public class UserForInitialization
    {
		public string Id { get; set; }
		public string Username { get; set; }
        public string Password { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }

    public class TestBrUsers
    {
        public static IEnumerable<UserForInitialization> GetUsersInfo()
        {
            var list = new List<UserForInitialization>
            {
                new UserForInitialization()
                {
	                Id =  "00000000-0000-0000-0000-00000000000A",
					Username = "empty",
                    Password = "123",
                    Roles = new List<string>(){}
                },
                new UserForInitialization()
                {
	                Id =  "00000000-0000-0000-0000-000000000001",
					Username = "tony",
                    Password = "123",
                    Roles = new List<string>(){ "User", "Author"}
                },
                new UserForInitialization()
                {
                    Id =  "00000000-0000-0000-0000-000000000003",
                    Username = "test",
                    Password = "321",
                    Roles = new List<string>() {"User"}
                }
            };

            return list;
        }

        public static BrUser GetUser()
        {
            return new BrUser()
            {
                Id = Guid.Parse("00000000-0000-0000-0000-000000000003"),
                Name = "Test",
                UserName = "test"
            };
        }
    }
}
