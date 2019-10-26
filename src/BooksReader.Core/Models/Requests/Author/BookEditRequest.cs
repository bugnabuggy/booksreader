using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.Requests.Author
{
    public class BookEditRequest: IIdentified
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public string Description { get; set; }

        public bool IsPublished { get; set; }
        public bool IsForSale { get; set; }

        public string Picture { get; set; }
    }
}
