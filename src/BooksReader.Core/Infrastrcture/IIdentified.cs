using System;
using System.Collections.Generic;
using System.Text;

namespace BooksReader.Core.Models
{
	public interface IIdentified
	{
		Guid Id { get; set; }
	}
}
