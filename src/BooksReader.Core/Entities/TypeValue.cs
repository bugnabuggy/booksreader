using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BooksReader.Core.Entities
{
    public class TypeValue
    {
        [Key]
        public uint Id { get; set; }
        [Required]
        [MaxLength(120)]
        public string Name { get; set; }
        [MaxLength(60)]
        public string LocalizationKey { get; set; }
        public string Value { get; set; }

        public ushort TypeId { get; set; }
        [ForeignKey("TypeId")]
        public TypesList Type { get; set; }
    }
}
