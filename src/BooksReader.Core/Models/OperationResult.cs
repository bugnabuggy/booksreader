using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models
{
    public class OperationResult : IOperationResult<object>
    {
        public OperationResult()
        {
            Messages = new List<string>();
        }

        public object Data { get; set; }
        public bool Success { get; set; }
        public IList<string> Messages { get; set; }
    }

    public class OperationResult<T> : OperationResult, IOperationResult<T>
    {
        public OperationResult():base()
        {}

        public new T Data { get; set; }
    }
}
