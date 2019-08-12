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
	public class CRUDService<T> : ICRUDOperatonService<T> where T: IIdentified
    {
        private readonly IRepository<T> _repo;
        public CRUDService(IRepository<T> repo)
        {
            _repo = repo;
        }

        public virtual IQueryable<T> Get()
        {
            return _repo.Data;
        }

        public virtual T Get(Guid id)
        {
            return _repo.Data.FirstOrDefault(x => x.Id.Equals(id));
        }

        public virtual IOperationResult<T> Add(T item)
        {
            var data = _repo.Add(item);

            return new OperationResult<T>()
            {
                Data = data,
                Success = true
            };
        }

		public virtual IOperationResult<T> Edit(T item)
        {
            var data = _repo.Update(item);

            return new OperationResult<T>()
            {
                Data = data,
                Success = true
            };
        }

		public virtual IOperationResult<T> Delete(Guid id)
        {
            var item = _repo.Data.FirstOrDefault(x => x.Id.Equals(id));
            var data = _repo.Delete(item);

            return new OperationResult<T>()
            {
                Data =  data,
                Success =  true
            };
        }

		public virtual async Task<IOperationResult<T>> AddAsync(T item)
        {
            var data = await _repo.AddAsync(item);
            return new OperationResult<T>()
            {
                Data = data,
                Success = true
            };
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
