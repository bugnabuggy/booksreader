using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksReader.Core.Models.DTO
{
    public interface IOrderingFilter
    {
        string OrderByField { get; set; }
        bool IsDesc { get; set; }
    }
}
