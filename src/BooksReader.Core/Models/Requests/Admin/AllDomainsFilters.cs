using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.Requests.Admin
{
    public class AllDomainsFilters : StandardFilters
    {
        public string Name { get; set; }
        public string Username { get; set; }
    }
}
