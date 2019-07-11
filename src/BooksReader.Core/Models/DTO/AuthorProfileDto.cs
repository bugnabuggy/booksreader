using System;

namespace BooksReader.Core.Models.DTO
{
    public class AuthorProfileDto
    {
        public Guid? Id { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }
        public string DomainName { get; set; }
        public string UrlPath { get; set; }
        public string PageContent { get; set; }
    }
}
