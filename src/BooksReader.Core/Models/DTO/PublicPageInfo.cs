using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;

namespace BooksReader.Core.Models.DTO
{
    public class PublicPageInfo
    {
        public string Content { get; set; }
        public string Path { get; set; }
        public SeoInfo SeoInfo { get; set; }
    }
}
