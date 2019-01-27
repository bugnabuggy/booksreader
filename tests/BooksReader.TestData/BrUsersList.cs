using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BooksReader.TestData
{
    class UserForInitialization
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public IEnumerable<string> Roles { get; set; }
    }

    class BrUsersList
    {
        public static IEnumerable<UserForInitialization> GetUsers()
        {
            var list = new List<UserForInitialization>
            {
                new UserForInitialization()
                {
                    Username = "empty",
                    Password = "123",
                    Roles = new List<string>(){}
                },
                new UserForInitialization()
                {
                    Username = "tony",
                    Password = "123",
                    Roles = new List<string>(){ "User", "Author"}
                },
                new UserForInitialization()
                {
                    Username = "test",
                    Password = "321",
                    Roles = new List<string>() {"User"}
                }
            };

            return list;
        }
    }
}
