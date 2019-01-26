using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models
{
    public class OperationResult 
    {
        public object Data { get; set; }
        public bool Success { get; set; }
        public IList<string> Messages { get; set; }
    }

    public class OperationResult<T> : OperationResult
    {
        public T Data { get; set; }
    }
}
