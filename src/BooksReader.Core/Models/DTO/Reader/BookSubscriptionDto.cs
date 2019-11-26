using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Enums;

namespace BooksReader.Core.Models.DTO.Reader
{
    public class BookSubscriptionDto
    {
        public Guid SubscriptionId { get; set; }
        public Guid BookId { get; set; }
        public DateTimeOffset StartDate { get; set; }
        public DateTimeOffset? EndDate { get; set; }
        public SubscriptionStatus Status { get; set; }
    }
}
