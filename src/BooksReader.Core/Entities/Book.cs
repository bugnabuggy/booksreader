using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;
using BooksReader.Core.Models;

namespace BooksReader.Core.Entities
{
	public class Book : IIdentified, IOwned
	{
		public Guid Id { get; set; }
		public Guid OwnerId { get; set; }

        [MaxLength(1000)]
		public string Title { get; set; }
        [MaxLength(1000)]
        public string Author { get; set; }

        [MaxLength(3000)]
        public string Description { get; set; }

        public bool IsPublished { get; set; }
        public bool IsForSale { get; set; }

        public string Picture { get; set; }

		public DateTime Created { get; set; }
		public DateTime? Published { get; set; }

		// non database properties
		public string OwnerName;
		public string OwnerUserName;

	}
}
