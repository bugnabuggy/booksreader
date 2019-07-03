using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BooksReader.Web.Models
{
	public class BookEditRequest
	{
		public Guid Id { get; set; }
		public string Title { get; set; }
		public string Author { get; set; }
	}
}
