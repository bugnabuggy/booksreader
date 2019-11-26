using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.DTO.Admin
{
    public class AdminBookDto
    {
        public Guid BookId { get; set; }
        public string BookTitle { get; set; }
        public string Username { get; set; }
        public DateTimeOffset Created { get; set; }
        public DateTimeOffset? Published { get; set; }
        public bool IsPublished { get; set; }
        public bool Verified { get; set; }

    }
}
