using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BooksReader.Core.Entities
{
    public class TypeValue
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int Id { get; set; }
        [Required]
        [MaxLength(120)]
        public string Name { get; set; }
        [MaxLength(60)]
        public string LocalizationKey { get; set; }
        public string Value { get; set; }

        public short TypeId { get; set; }
        [ForeignKey("TypeId")]
        public TypesList Type { get; set; }
    }
}
