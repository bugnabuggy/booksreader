using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;

namespace BooksReader.Core.Models.DTO.Author
{
    public class BookEditDto
    {
        public Book Book { get; set; }
        public PublicPageInfo PublicPageInfo { get; set; }
        public IEnumerable<BookChapter> BookChapters { get; set; }
    }
}
