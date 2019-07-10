using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.Requests
{
    public class AuthorProfileRequest
    {
        public Guid ProfileId { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public string DomainName { get; set; }
        public string UrlPath { get; set; }
        public string PageContent { get; set; }
    }
}
