using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;

namespace BooksReader.Core.Models.DTO.Author
{
    public class AuthorProfileDto
    {
        public Guid? Id { get; set; }
        public string AuthorName { get; set; }
        public string Description { get; set; }

        public IEnumerable<UserDomain> Domains { get; set; }
        public PublicPage Page { get; set; }
    }
}
