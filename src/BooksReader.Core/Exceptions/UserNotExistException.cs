using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Exceptions
{
    public  class UserNotExistException : Exception
    {
        public string Username { get; }

        public UserNotExistException(string username)
        {
            Username = username;
        }
    }
}
