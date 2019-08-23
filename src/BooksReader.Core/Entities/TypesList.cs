using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BooksReader.Core.Models;

namespace BooksReader.Core.Entities
{
    public class TypesList 
    {
        public ushort Id { get; set; }

        [MaxLength(120)]
        public string Name { get; set; }

        [MaxLength(60)]
        public string LocalizationKey { get; set; }
    }
}
