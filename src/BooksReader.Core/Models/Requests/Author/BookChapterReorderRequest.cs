using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.Requests.Author
{
    public class BookChapterReorderRequest
    {
        public Guid Id { get; set; }
        public uint Number { get; set; }
    }
}
