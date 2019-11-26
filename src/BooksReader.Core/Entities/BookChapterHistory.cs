using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using BooksReader.Core.Models;

namespace BooksReader.Core.Entities
{
    public class BookChapterHistory : IIdentified, IOwned
    {
        public Guid Id { get; set; }
        public Guid OwnerId { get; set; }

        public Guid ChapterId { get; set; }

        public DateTimeOffset Date { get; set; }
        public uint Version { get; set; }
        public string Content { get; set; }
        [MaxLength(1000)]
        public string Title { get; set; }

        [ForeignKey("ChapterId")]
        public BookChapter BookChapter { get; set; }
    }
}
