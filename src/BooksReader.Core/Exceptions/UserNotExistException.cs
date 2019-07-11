using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Exceptions
{
    public  class UserNotExistException : BaseBrException
    {
        public string Username { get; }

        public UserNotExistException(string username)
        {
            Username = username;
        }
    }
}
