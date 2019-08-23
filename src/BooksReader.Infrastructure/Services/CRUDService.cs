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
        protected readonly IRepository<T> Repository;

        public CRUDService(IRepository<T> repository)
        {
            this.Repository = repository;
        }

        public virtual IQueryable<T> Get()
        {
            return Repository.Data;
        }

        public virtual T Get(Guid id)
        {
            return Repository.Data.FirstOrDefault(x => x.Id.Equals(id));
        }

        public virtual IOperationResult<T> Add(T item)
        {
            var data = Repository.Add(item);

            return new OperationResult<T>()
            {
                Data = data,
                Success = true
            };
        }

		public virtual IOperationResult<T> Edit(T item)
        {
            var data = Repository.Update(item);

            return new OperationResult<T>()
            {
                Data = data,
                Success = true
            };
        }

		public virtual IOperationResult<T> Delete(Guid id)
        {
            var item = Repository.Data.FirstOrDefault(x => x.Id.Equals(id));
            var data = Repository.Delete(item);

            return new OperationResult<T>()
            {
                Data =  data,
                Success =  true
            };
        }

		public virtual async Task<IOperationResult<T>> AddAsync(T item)
        {
            var data = await Repository.AddAsync(item);
            return new OperationResult<T>()
            {
                Data = data,
                Success = true
            };
        }


		public virtual Task<IEnumerable<T>> GetAsync()
        {
            return Repository.GetAsync(x=>true, null, null);
        }

		public virtual Task<T> GetAsync(Guid id)
        {
            return Repository.Data.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }
		
	}
}
