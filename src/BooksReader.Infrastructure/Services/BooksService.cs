using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Exceptions;
using BooksReader.Core.Models;
using BooksReader.Core.Services;
using BooksReader.Infrastructure.Models;
using BooksReader.Infrastructure.Repositories;
using IdentityServer4.Extensions;

namespace BooksReader.Infrastructure.Services
{
	public class BooksService : IBooksService
	{
		private readonly IRepository<Book> _bookRepo;

		public BooksService(
			IRepository<Book> bookRepo
		)
		{
			_bookRepo = bookRepo;
		}

		public Book Add(Book item)
		{
			if (item.Title.IsNullOrEmpty())
			{
				throw new BrBadDataException("Title can't be empty");
			}

			var book = _bookRepo.Add(item);
			return book;
		}

		public Book Edit(Book item)
		{
			if (item.Title.IsNullOrEmpty())
			{
				throw new BrBadDataException("Title can't be empty");
			}
			Book newBook = this.Get(item.Id);
			newBook.Title = item.Title;
			newBook.Author = item.Author;
			var book = _bookRepo.Update(newBook);
			return book;
		}

		public IQueryable<Book> Get()
		{
			return _bookRepo.Data;
		}

		public IQueryable<Book> Get(string userId)
		{
			return _bookRepo.Data.Where(x => x.OwnerId.ToString() == userId);
		}

		public Book Get(Guid id)
		{
			return _bookRepo.Data.FirstOrDefault(x => x.Id == id);
		}

		public Book Delete(string id)
		{
			var item = this.Get(Guid.Parse(id));
			var book = _bookRepo.Delete(item);
			return book;
		}

		public Task<Book> AddAsync(Book item)
		{
			throw new NotImplementedException();
		}

		public Task<Book> EditAsync(Book item)
		{
			throw new NotImplementedException();
		}

		public Task<IQueryable<Book>> GetAsync()
		{
			throw new NotImplementedException();
		}

		public Task<Book> GetAsync(Guid iid)
		{
			throw new NotImplementedException();
		}

		public Task<Book> DeleteAsync(Book item)
		{
			throw new NotImplementedException();
		}
	}
}
