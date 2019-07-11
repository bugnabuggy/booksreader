﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Infrastructure.DataContext;
using Microsoft.EntityFrameworkCore;

namespace BooksReader.Infrastructure.Repositories
{
    public class DbRepository<T> : IRepository<T> where T : class
    {
        private BrDbContext _ctx;
        private DbSet<T> _table;
        public IQueryable<T> Data { get; }

        public DbRepository(BrDbContext ctx)
        {
            _ctx = ctx;
            _table = _ctx.Set<T>();
            Data = _table.AsNoTracking();
        }

        public T Update(T entity)
        {
            _table.Update(entity);
            _ctx.SaveChanges();
            return entity;
        }

        public T Add(T entity)
        {
            _table.Add(entity);
            _ctx.SaveChanges();
            return entity;
        }

        public T Delete(T entity)
        {
            _table.Remove(entity);
            _ctx.SaveChanges();
            return entity;
        }

        public IEnumerable<T> Get(
           Expression<Func<T, bool>> filter = null,
           Func<IQueryable<T>, IOrderedQueryable<T>> orderBy = null,
           string includeProperties = "")
        {
            IQueryable<T> query = this.Data.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split
                (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                return orderBy(query).ToList();
            }
            else
            {
                return query.ToList();
            }
        }

        public IEnumerable<T> Add(IEnumerable<T> items)
        {
            _table.AddRange(items);
            _ctx.SaveChanges();
            return items;
        }

        public IEnumerable<T> Update(IEnumerable<T> items)
        {
            _table.UpdateRange(items);
            _ctx.SaveChanges();
            return items;
        }

        public IEnumerable<T> Delete(IEnumerable<T> items)
        {
            _table.RemoveRange(items);
            _ctx.SaveChanges();
            return items;
        }

        public async Task<T> AddAsync(T item)
        {
            await _table.AddAsync(item);
            await _ctx.SaveChangesAsync();
            return item;
        }

        public async Task<IEnumerable<T>> AddAsync(IEnumerable<T> items)
        {
            await _table.AddRangeAsync(items);
            await _ctx.SaveChangesAsync(true);
            
            return items;
        }

        public async Task<IEnumerable<T>> GetAsync(
                Expression<Func<T, bool>> filter,
                Func<IQueryable<T>, IOrderedQueryable<T>> orderBy,
                string includeProperties)
        {
            IQueryable<T> query = this.Data.AsNoTracking();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            if (!string.IsNullOrEmpty(includeProperties))
            {
                foreach (var includeProperty in includeProperties.Split
                    (new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
                {
                    query = query.Include(includeProperty);
                }
            }

            if (orderBy != null)
            {
                return await orderBy(query).ToListAsync();
            }
            else
            {
                return await query.ToListAsync();
            }
        }


    }
}
