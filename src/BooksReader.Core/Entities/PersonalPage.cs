using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using BooksReader.Core.Enums;
using BooksReader.Core.Models;

namespace BooksReader.Core.Entities
{
    public class PersonalPage: IIdentified
    {
        public Guid Id { get; set; }

        public PersonalPageType PageType { get; set; }

        [MaxLength(256)]
        public string Domain { get; set; }

        [MaxLength(256)]
        public string UrlPath { get; set; }

        public string Content { get; set; }

        public Guid? SeoInfoId { get; set; }


        /* Navigation properties */
        [ForeignKey("SeoInfoId")]
        public SeoInfo SeoInfo{ get; set; }

    }
}
