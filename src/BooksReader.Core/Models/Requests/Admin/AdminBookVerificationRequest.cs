using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.Requests.Admin
{
    public class AdminBookVerificationRequest
    {
        public Guid BookId { get; set; }
        public bool Verified { get; set; }
    }
}
