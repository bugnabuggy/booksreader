using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Models.DTO;

namespace BooksReader.Core.Models.Requests.Filters
{
    public class AuthorBookFiltersRequest : StandardFiltersDto
    {
        public string Title { get; set; }
    }
}
