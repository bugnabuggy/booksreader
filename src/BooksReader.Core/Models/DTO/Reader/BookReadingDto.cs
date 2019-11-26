using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.DTO.Reader
{
    public class BookReadingDto 
    {
        public GeneralBook Book { get; set; }
        public IEnumerable<ChapterReadingDto> Chapters { get; set; }
        public string SessionId { get; set; }
    }
}
