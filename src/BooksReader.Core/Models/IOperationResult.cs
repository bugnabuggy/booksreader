using System.Collections.Generic;

namespace BooksReader.Core.Models
{
    public interface IOperationResult<T>
    {
        T Data { get; set; }
        bool Success { get; set; }
        IList<string> Messages { get; set; }
    }
}