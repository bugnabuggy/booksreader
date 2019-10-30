using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.Requests.Author
{
    public class BookChapterRequest : IIdentified
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public bool IsPublished { get; set; }
        public string Content { get; set; }
    }

}