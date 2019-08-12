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

        public WebResult(){}

        public WebResult(OperationResult result)
        {
            base.Success = result.Success;
            base.Messages = result.Messages;
            base.Data = result.Data;
        }
    }

    public class WebResult<T> : WebResult
    {
        public new T Data { get; set; }
    }
}
