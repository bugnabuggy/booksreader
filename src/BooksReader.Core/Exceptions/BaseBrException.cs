using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Exceptions
{
    public class BaseBrException : Exception
    {
        public BaseBrException(string message = ""): base(message)
        {
            
        }
    }
}
