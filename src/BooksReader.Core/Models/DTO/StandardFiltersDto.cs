using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksReader.Core.Models.DTO
{
    public class StandardFiltersDto : IOrderingFilter, IPaginationFilter
    {
        public string OrderByField { get; set; }
        public bool IsDesc { get; set; }
        public int? PageSize { get; set; }
        public int? PageNumber { get; set; }
    }
}
