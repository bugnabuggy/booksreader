﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace BooksReader.Core.Infrastrcture
{
    public interface IRepository<T>
    {
        IQueryable<T> Data { get; }

        T Update(T item);
        T Add(T item);
        T Delete(T item);

        IEnumerable<T> Get(Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
            string includeProperties = "");

        IEnumerable<T> Add(IEnumerable<T> items);
        IEnumerable<T> Update(IEnumerable<T> items);
        IEnumerable<T> Delete(IEnumerable<T> items);


        Task<T> AddAsync(T item);
        Task<IEnumerable<T>> AddAsync(IEnumerable<T> items);

        Task<IEnumerable<T>> GetAsync(Expression<Func<T, bool>> filter,
            Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
            string includeProperties);

    }
}
