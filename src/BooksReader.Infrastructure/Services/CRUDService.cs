using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Models;
using BooksReader.Core.Services;

namespace BooksReader.Infrastructure.Services
{
	public class CRUDService<T> : ICRUDService<T> where T: IIdentified
	{
		public T Add(T item)
		{
			throw new NotImplementedException();
		}

		public T Edit(T item)
		{
			throw new NotImplementedException();
		}

		public IQueryable<T> Get()
		{
			throw new NotImplementedException();
		}

		public T Get(Guid iid)
		{
			throw new NotImplementedException();
		}

		public T Delete(Guid id)
		{
			throw new NotImplementedException();
		}

		public Task<T> AddAsync(T item)
		{
			throw new NotImplementedException();
		}

		public Task<T> EditAsync(T item)
		{
			throw new NotImplementedException();
		}

		public Task<IQueryable<T>> GetAsync()
		{
			throw new NotImplementedException();
		}

		public Task<T> GetAsync(Guid iid)
		{
			throw new NotImplementedException();
		}

		public Task<T> DeleteAsync(T item)
		{
			throw new NotImplementedException();
		}
	}
}
