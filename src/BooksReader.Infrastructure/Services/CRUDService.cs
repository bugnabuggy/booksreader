using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Infrastructure;
using BooksReader.Core.Models;
using BooksReader.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace BooksReader.Infrastructure.Services
{
	public class CRUDService<T> : ICRUDService<T> where T: IIdentified
    {
        private readonly IRepository<T> _repo;
        public CRUDService(IRepository<T> repo)
        {
            _repo = repo;
        }

		public virtual T Add(T item)
        {
            var result = _repo.Add(item);
            return result;
        }

		public virtual T Edit(T item)
        {
            var result = _repo.Update(item);
            return result;
        }

		public virtual IQueryable<T> Get()
        {
            return _repo.Data;
        }

		public virtual T Get(Guid id)
        {
            return _repo.Data.FirstOrDefault(x => x.Id.Equals(id));
        }

		public virtual T Delete(Guid id)
        {
            var item = _repo.Data.FirstOrDefault(x => x.Id.Equals(id));
            var result = _repo.Delete(item);
            return result;
        }

		public virtual Task<T> AddAsync(T item)
        {
            return _repo.AddAsync(item);
        }


		public virtual Task<IEnumerable<T>> GetAsync()
        {
            return _repo.GetAsync(x=>true, null, null);
        }

		public virtual Task<T> GetAsync(Guid id)
        {
            return _repo.Data.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
		
	}
}
