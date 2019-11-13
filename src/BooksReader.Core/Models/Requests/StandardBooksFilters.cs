using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.Requests
{
    public class StandardBooksFilters : StandardFilters
    {
        public Guid? UserId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }
        public string PublishDate { get; set; }
        public bool? IsPublished { get; set; }
    }
}
