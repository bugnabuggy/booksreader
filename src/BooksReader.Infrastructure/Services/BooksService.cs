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
using Microsoft.EntityFrameworkCore;

namespace BooksReader.Infrastructure.Services
{
	public class BooksService : IBooksService
	{
		private readonly IRepository<Book> _bookRepo;
		private readonly IRepository<BrUser> _userRepo;

		public BooksService(
			IRepository<Book> bookRepo,
			IRepository<BrUser> userRepo
		)
		{
			_bookRepo = bookRepo;
			_userRepo = userRepo;
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

		private IQueryable<Book> JoinWithOwner(IQueryable<Book> bookQuery)
		{
			return bookQuery.Join(this._userRepo.Data, x => x.OwnerId.ToString(), y => y.Id, (x, y) =>
					new Book()
					{
						Id = x.Id,
						Title = x.Title,
						OwnerId = x.OwnerId,
						Author = x.Author,
						Created = x.Created,
						Published = x.Published,
						OwnerUserName = y.UserName,
						OwnerName = y.Name
					}
				);
		}

		public IQueryable<Book> Get()
		{
			return JoinWithOwner(_bookRepo.Data);
				
		}

		public IQueryable<Book> Get(string userId)
		{
			return JoinWithOwner(_bookRepo.Data.Where(x => x.OwnerId.ToString() == userId));
		}

		public Book Get(Guid id)
		{
			return JoinWithOwner(_bookRepo.Data.Where(x => x.Id == id)).FirstOrDefault();
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
