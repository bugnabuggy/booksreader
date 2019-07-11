using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Exceptions
{
    public class ItemEditingException<T>: BaseBrException
    {
        public T Item { get; }
        public IEnumerable<string> Messages { get; }
        public ItemEditingException(T item, string message = "", IEnumerable<string> messages = null) : base(message)
        {
            Item = item;
            Messages = messages;
        }
    }
}
