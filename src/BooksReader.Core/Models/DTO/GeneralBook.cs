using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models.DTO
{
    public class GeneralBook
    {   
        public Guid BookId { get; set; }
        
        public string Title { get; set; }
        public string Author { get; set; }
        
        public bool IsForSale { get; set; }
        public string Picture { get; set; }
    }
}
