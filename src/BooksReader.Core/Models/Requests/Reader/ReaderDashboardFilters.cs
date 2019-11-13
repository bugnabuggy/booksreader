using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.Requests.Reader
{
    public class ReaderDashboardFilters : StandardBooksFilters
    {
        public string SubscriptionStartDate { get; set; }
        public string SubscriptionEndDate { get; set; }
    }
}
