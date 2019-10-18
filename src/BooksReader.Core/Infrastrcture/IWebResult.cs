using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Infrastrcture
{
    public interface IWebResult<T> : IOperationResult<T>, IPaginationFilter
    {
        long Total { get; set; }
    }

    public interface IWebResult : IWebResult<object>
    {
        
    }
}
