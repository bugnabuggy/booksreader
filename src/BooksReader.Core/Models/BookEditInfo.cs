using System.Collections.Generic;
using BooksReader.Core.Entities;

namespace BooksReader.Core.Models
{
    public class BookEditInfo
    {
        public Book Book { get; set; }
        public PersonalPage BookPage{ get; set; }
        public IEnumerable<BookChapter> Chapters { get; set; }
        public IEnumerable<BookPrice> Prices { get; set; }
    }
}
