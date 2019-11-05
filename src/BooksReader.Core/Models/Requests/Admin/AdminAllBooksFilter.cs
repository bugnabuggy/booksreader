using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.Requests.Admin
{
    public class AdminAllBooksFilter: StandardFilters
    {
        public string Title { get; set; }
        public string Username { get; set; }
        public bool? IsPublished { get; set; }
        public bool? Verified { get; set; }

        public string PublishedFrom { get; set; }
        public string PublishedTo { get; set; }

        public string CreatedFrom { get; set; }
        public string CreatedTo { get; set; }
    }
}
