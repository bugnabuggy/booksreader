using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;

namespace BooksReader.Core.Models.DTO.Author
{
    public class BookFullEditInfoDto
    {
        public Book Book { get; set; }
        public PublicPage Page { get; set; }
        public IEnumerable<BookPrice> Prices { get; set; }
        public IEnumerable<BookChapter> Chapters { get; set; }
    }
}
