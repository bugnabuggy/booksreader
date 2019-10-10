using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Infrastrcture;

namespace BooksReader.Core.Models
{
    public class StandardFilters: IOrderingFilter, IPaginationFilter
    {
        public string OrderByField { get; set; }
        public bool? IsDesc { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}
