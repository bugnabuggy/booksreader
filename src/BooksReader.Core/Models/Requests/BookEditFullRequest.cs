using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.Requests
{

    public class BookFormSubRequest : BookEditRequest
    {
        public string Description { get; set; }
        public bool IsPublished { get; set; }
        public bool IsForSale { get; set; }
    }

    public class BookPricesRequest
    {
        public Guid Id { get; set; }
        public double Price { get; set; }
        public uint CurrencyId { get; set; }
    }

    public class PublicPageRequest
    {
        public string Domain { get; set; }
        public string UrlPath { get; set; }
        public string Content { get; set; }
    }

    public class BookEditFullRequest
    {
        public BookFormSubRequest Book { get; set; }
        public IEnumerable<BookPricesRequest> Prices { get; set; }
        public PublicPageRequest BookPage { get; set; }
    }
}
