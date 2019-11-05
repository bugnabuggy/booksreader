using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Enums;

namespace BooksReader.Core.Models.DTO.Public
{
    public class BookMarketDto
    {
        public Guid BookId { get; set; }
        public string Title { get; set; }
        public string Author { get; set; }
        public bool IsForSale { get; set; }
        public string Picture { get; set; }
        public DateTime? Published { get; set; }

        // not sure that it should be shown at market page
        public SubscriptionStatus Subscription { get; set; }

        public IEnumerable<BookPrice> BookPrices { get; set; }
    }
}
