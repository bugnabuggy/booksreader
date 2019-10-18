using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Infrastrcture
{
    public class WebResult<T> :  OperationResult<T>, IWebResult<T>
    {
        public long Total { get; set; }
        public int? PageNumber { get; set; }
        public int? PageSize { get; set; }

        public WebResult() { }

        public WebResult(OperationResult<T> result)
        {
            base.Success = result.Success;
            base.Messages = result.Messages;
            base.Data = result.Data;
        }
    }

    public class WebResult: WebResult<object>
    {
        public WebResult(OperationResult result)
        {
            base.Success = result.Success;
            base.Messages = result.Messages;
            base.Data = result.Data;
        }
    }
}
