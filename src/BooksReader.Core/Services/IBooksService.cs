using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Models;

namespace BooksReader.Core.Services
{
	public interface IBooksService : ICRUDService<Book>
	{
		IQueryable<Book> Get(string user);

	}
}
