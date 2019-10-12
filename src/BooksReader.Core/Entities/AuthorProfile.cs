﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;
using BooksReader.Core.Models;

namespace BooksReader.Core.Entities
{
    public class AuthorProfile: IIdentified
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        [MaxLength(256)]
        public string AuthorName { get; set; }

        [MaxLength(3000)]
        public string Description { get; set; }

        /* Navigation properties */
        [ForeignKey("UserId")]
        public BrUser User { get; set; }
    }
}
