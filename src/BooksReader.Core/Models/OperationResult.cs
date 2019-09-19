using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BooksReader.Core.Models
{
    public class OperationResult : IOperationResult
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
        public OperationResult() : base()
        {
        }

        public OperationResult(bool success) : base()
        {
            this.Success = success;
        }

        public OperationResult(IEnumerable<string> messages) : base()
        {
            this.Messages = messages.ToList();
        }

        public OperationResult(T data) : base()
        {
            this.Data = data;
        }


        public new T Data { get; set; }
    }
}
