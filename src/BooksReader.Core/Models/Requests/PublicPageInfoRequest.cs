using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.Requests
{
    public class PublicPageInfoRequest
    {
        public string Domain { get; set; }
        public string UrlPath { get; set; }
        public string PromoCode { get; set; }
    }
}
