using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Models;

namespace BooksReader.Core.Services
{
	public interface ICRUDOperatonService<T>  where T : IIdentified
	{
        T Get(Guid id);
        IQueryable<T> Get();

        IOperationResult<T> Add(T item);
        IOperationResult<T> Edit(T item);
        IOperationResult<T> Delete(Guid id);

        Task<T> GetAsync(Guid id);
        Task<IEnumerable<T>> GetAsync();
        Task<IOperationResult<T>> AddAsync(T item);
	}
}
