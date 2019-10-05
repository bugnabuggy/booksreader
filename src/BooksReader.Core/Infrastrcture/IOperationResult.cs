using System.Collections.Generic;

namespace BooksReader.Core.Infrastrcture
{
    public interface IOperationResult<T>
    {
        T Data { get; set; }
        bool Success { get; set; }
        IList<string> Messages { get; set; }
    }

    public interface IOperationResult: IOperationResult<object> { }
}