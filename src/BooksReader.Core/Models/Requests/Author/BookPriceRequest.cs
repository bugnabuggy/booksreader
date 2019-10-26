using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.Requests.Author
{
    public class BookPriceRequest: IIdentified
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public uint CurrencyId { get; set; }
        public double Price { get; set; }
    }
}
