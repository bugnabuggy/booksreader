using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksReader.Core.Models.DTO
{
    public interface  IPaginationFilter
    {
        int? PageSize { get; set; }
        int? PageNumber { get; set; }
    }
}
