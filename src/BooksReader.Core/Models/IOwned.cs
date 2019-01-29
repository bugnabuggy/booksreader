using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models
{
	public interface IOwned
	{
		Guid OwnerId { get; set; }
	}
}
