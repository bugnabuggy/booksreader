using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.DTO.Reader
{
    public class ChapterReadingDto
    {
        public Guid Id { get; set; }
        public uint Number { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Version { get; set; }
    }
}
