using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.Requests
{
    public class LoginHistoryRequest
    {
        public LoginHistoryCoordinates Coordinates { get; set; }
        public LoginHistoryScreen Screen { get; set; }
        public string UserAgent { get; set; }
    }
}
