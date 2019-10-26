using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Entities.Database
{
    public class BookChapterVersions
    {
        public uint Id { get; set; }

        public Guid BookId { get; set; }


        public Guid BookHistory { get; set; }
        public Guid BookChapterHistory { get; set; }
    }
}
