using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BooksReader.Core.Models;

namespace BooksReader.Core.Entities
{
    public class SeoInfo: IIdentified
    {
        public Guid Id { get; set; }

        [MaxLength(256)]
        public string MetaDescription { get; set; }

        [MaxLength(256)]
        public string MetaKeywords { get; set; }

        [MaxLength(256)]
        public string MetaTitle { get; set; }

        [MaxLength(256)]
        public string MetaAuthor { get; set; }

        [MaxLength(256)]
        public string MetaCopyright { get; set; }

        [MaxLength(256)]
        public string OgTitle { get; set; }

        [MaxLength(256)]
        public string OgUrl { get; set; }

        [MaxLength(256)]
        public string OgImage { get; set; }

        [MaxLength(256)]
        public string OgDescription { get; set; }

        [MaxLength(256)]
        public string OgType { get; set; }
    }
}
