using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.Requests
{
    public class BookChapterRequest
    {
        public Guid? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
    }
}
