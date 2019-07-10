﻿using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Exceptions
{
    public class ItemAlreadyExistsException<T> : Exception
    {
        public T Item { get; }
        public ItemAlreadyExistsException(T item, string message = ""): base(message)
        {
            Item = item;
        }
    }
}