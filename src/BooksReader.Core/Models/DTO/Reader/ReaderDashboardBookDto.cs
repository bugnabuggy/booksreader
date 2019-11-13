using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;

namespace BooksReader.Core.Models.DTO.Reader
{
    public class ReaderDashboardBookDto 
    {
        public GeneralBook Book { get; set; }
        public BookSubscriptionDto Subscription { get; set; }
    }
}
