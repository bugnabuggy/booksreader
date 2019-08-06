using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Exceptions;
using BooksReader.Core.Services;
using BooksReader.Infrastructure.Repositories;

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
			if (string.IsNullOrEmpty(item.Title))
			{
				throw new BrBadDataException("Title can't be empty");
			}

			var book = _bookRepo.Add(item);
			return book;
		}

		public Book Edit(Book item)
		{
			if (string.IsNullOrEmpty(item.Title))
			{
				throw new BrBadDataException("Title can't be empty");
			}
			
			var book = _bookRepo.Update(item);
			return book;
		}

		private IQueryable<Book> JoinWithOwner(IQueryable<Book> bookQuery)
		{
			return bookQuery.Join(this._userRepo.Data, x => x.OwnerId, y => y.Id, (x, y) =>
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

		public IQueryable<Book> GetByOwnerId(Guid ownerId)
		{
			return JoinWithOwner(_bookRepo.Data.Where(x => x.OwnerId.Equals(ownerId)));
		}

		public Book Get(Guid id)
		{
			return JoinWithOwner(_bookRepo.Data.Where(x => x.Id == id)).FirstOrDefault();
		}

		public Book Delete(Guid id)
		{
			var item = this.Get(id);
			var book = _bookRepo.Delete(item);
			return book;
		}

		public async  Task<Book> AddAsync(Book item)
		{
            if (string.IsNullOrEmpty(item.Title))
            {
                throw new BrBadDataException("Title can't be empty");
            }

            var book = await _bookRepo.AddAsync(item);
            return book;
        }

		public Task<Book> EditAsync(Book item)
		{
            if (string.IsNullOrEmpty(item.Title))
            {
                throw new BrBadDataException("Title can't be empty");
            }

            var book = _bookRepo.Update(item);
            return book;
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
