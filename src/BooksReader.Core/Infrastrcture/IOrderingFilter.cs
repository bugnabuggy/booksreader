using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Infrastrcture
{
    public interface IOrderingFilter
    {
        string OrderByField { get; set; }
        bool? IsDesc { get; set; }
    }
}
