using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.DTO.Reader
{
    public class BookSubscriptionDto
    {
        public Guid SubscriptionId { get; set; }
        public Guid BookId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime? EndDate { get; set; }
    }
}
