using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace BooksReader.Core.Entities
{
    public class TypesList
    {
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public short Id { get; set; }

        [MaxLength(120)]
        public string Name { get; set; }

        [MaxLength(60)]
        public string LocalizationKey { get; set; }

        public virtual IEnumerable<TypeValue> Values { get; set; }
    }
}
