using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Models;

namespace BooksReader.Core.Entities
{
	public class Book : IIdentified, IOwned
	{
		public Guid Id { get; set; }
		public Guid OwnerId { get; set; }

		public string Title { get; set; }
		public string Author { get; set; }

		public DateTime Created { get; set; }
		public DateTime Published { get; set; }
	}
}
