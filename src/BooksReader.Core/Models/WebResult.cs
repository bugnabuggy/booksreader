using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models
{
    public class WebResult : OperationResult
    {
        public int Total { get; set; }
        public int PageNumber { get; set; }
        public int PageSize { get; set; }
    }

    public class WebResult<T> : WebResult
    {
        public T Data { get; set; }
    }
}
