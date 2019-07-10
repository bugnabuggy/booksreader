using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Exceptions
{
    public class NotFoundException<T>: Exception
    {
        public T Item { get;}
        public NotFoundException(T item, string message = ""):base(message)
        {
            Item = item;
        }
    }
}
