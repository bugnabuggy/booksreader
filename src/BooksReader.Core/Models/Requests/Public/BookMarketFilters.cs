using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.Requests.Public
{
    public class BookMarketFilters : StandardFilters
    {
        public string Title { get; set; }
        public string Author { get; set; }

        public bool? IsForSale { get; set; }
        public double? PriceFrom { get; set; }
        public double? PriceTo { get; set; }
        public ushort? SubscriptionDurationFrom { get; set; }
        public ushort? SubscriptionDurationTo { get; set; }
    }
}
