using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Infrastrcture
{
    public interface IPaginationFilter
    {
        int? PageSize { get; set; }
        int? PageNumber { get; set; }
    }
}
