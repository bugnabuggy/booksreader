using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Enums;
using BooksReader.Core.Infrastrcture;

namespace BooksReader.Core.Models.DTO.Public
{
    public class BookMarketDto : GeneralBook, ISemanticUrl
    {
        public string SemanticUrl { get; set; }
        public string AuthorSemanticUrl { get; set; }

        public Guid AuthorId { get; set; }
        public DateTimeOffset? Published { get; set; }
        public int SubscriptionDurationDays { get; set; }

        // not sure that it should be shown at market page
        public SubscriptionStatus Subscription { get; set; }

        public IEnumerable<BookPrice> BookPrices { get; set; }
    }
}
