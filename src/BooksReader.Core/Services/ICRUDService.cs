using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Models;

namespace BooksReader.Core.Services
{
	public interface ICRUDService<T>  where T : IIdentified
	{
		T Add(T item);
		T Edit(T item);
		IQueryable<T> Get();
		T Get(Guid id);
		T Delete(Guid id);

		Task<T> AddAsync(T item);
		Task<IEnumerable<T>> GetAsync();
		Task<T> GetAsync(Guid id);
	}
}
