using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BooksReader.Core.Entities;
using BooksReader.Core.Enums;
using BooksReader.Core.Exceptions;
using BooksReader.Core.Infrastructure;
using BooksReader.Core.Models;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Services;
using BooksReader.Dictionaries.Messages;
using Microsoft.EntityFrameworkCore;

namespace BooksReader.Services
{
	public class BooksService : IBooksService
	{
		private readonly IRepository<Book> _bookRepo;
		private readonly IRepository<BrUser> _userRepo;
        private readonly IRepository<BookChapter> _bookChaptersRepo;

        private readonly IPersonalPageService _personalPageService;
        private readonly IBookPriceService _bookPriceService;
        private readonly IMapper _mapper;

        public BooksService(
			IRepository<Book> bookRepo,
			IRepository<BrUser> userRepo,
            IRepository<BookChapter> bookChaptersRepo,
            IPersonalPageService personalPageService,
            IBookPriceService bookPriceService,
            IMapper mapper
        )
		{
			_bookRepo = bookRepo;
			_userRepo = userRepo;
            _bookChaptersRepo = bookChaptersRepo;
            _personalPageService = personalPageService;
            _bookPriceService = bookPriceService;
            _mapper = mapper;
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
						OwnerName = y.Name,
                        Description = x.Description,
                        IsPublished = x.IsPublished,
                        IsForSale = x.IsForSale,
                        Picture = x.Picture
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

		public async Task<IEnumerable<Book>> GetAsync()
        {
            var books = _bookRepo.Data;
            return await JoinWithOwner(books).ToListAsync();
        }

		public Task<Book> GetAsync(Guid id)
        {
            var book = _bookRepo.Data.Where(x => x.Id.Equals(id));
            return JoinWithOwner(book).FirstOrDefaultAsync();
        }

        public IOperationResult<BookEditInfo> GetFull(Guid bookId)
        {
            var book = Get(bookId);

            var bookChapters = _bookChaptersRepo.Data.Where(x => x.BookId.Equals(bookId));

            var personalPage = _personalPageService.Get()
                .FirstOrDefault(x =>
                                    x.PageType == PersonalPageType.BookPage
                                    && x.SubjectId.Equals(bookId)
                );

            var prices = _bookPriceService.GetByBookId(book.Id);

            var data = new BookEditInfo()
            {
                Book = book,
                BookPage = personalPage,
                Chapters = bookChapters.OrderBy(x => x.Number),
                Prices = prices
            };

            return new OperationResult<BookEditInfo>()
            {
                Data = data,
                Success = true,
            };
        }

        public IOperationResult<BookEditFullRequest> SaveFull(BookEditFullRequest bookInfo)
        {
            var result = new OperationResult<BookEditFullRequest>(bookInfo)
            {
                Success = true
            };

            if (string.IsNullOrWhiteSpace(bookInfo.BookPage.Content)
                && string.IsNullOrWhiteSpace(bookInfo.BookPage.Domain)
                && string.IsNullOrWhiteSpace(bookInfo.BookPage.UrlPath))
            {
                bookInfo.BookPage = null;
            }

            var validations = new List<string>();
            validations.AddRange(Validate(bookInfo));
            if (validations.Any())
            {
                result.Messages = validations;
                result.Success = false;
                // no reason to continue saving if validations failed
                goto end;
            }

            // save book 
            var bookResult  = Save(bookInfo.Book);
            result.Success = bookResult.Success;
            validations.AddRange(result.Messages);

            if (bookInfo.BookPage != null)
            {
                var pageResult =
                    _personalPageService.Save(bookInfo.BookPage, bookInfo.Book.Id, PersonalPageType.BookPage);
                result.Success = pageResult.Success;
                validations.AddRange(pageResult.Messages);
            }

            if (bookInfo.Prices.Any())
            {
                var pricesResult = _bookPriceService.SetPrices(bookInfo.Prices, bookInfo.Book.Id);

                result.Success = pricesResult.Success;
                validations.AddRange(pricesResult.Messages);
            }

            result.Messages = validations;

            end:
            return result;
        }

        public IEnumerable<string> Validate(Book item)
        {
            var validationMessages = new List<string>();

            if (string.IsNullOrEmpty(item.Title))
            {
                validationMessages.Add("Title can't be empty");
            }

            return validationMessages;
        }


        public IEnumerable<string> Validate(BookEditFullRequest item)
        {
            var validationMessages = new List<string>();

            // validate book
            var book = _mapper.Map<Book>(item.Book);
            validationMessages.AddRange(Validate(book));

            // validate public page
            if (item.BookPage != null)
            {
                var page = _mapper.Map<PersonalPage>(item.BookPage);
                validationMessages.AddRange(_personalPageService.Validate(page));
            }

            // validate prices
            foreach (var bookPriceRequest in item.Prices ?? new List<BookPricesRequest>())
            {
                var bookPrice = _mapper.Map<BookPrice>(bookPriceRequest);
                validationMessages.AddRange(_bookPriceService.Validate(bookPrice));
            }

            return validationMessages;

        }

        private IOperationResult<Book> Save(BookFormSubRequest item)
        {
            var result = new OperationResult<Book>();

            var book = _bookRepo.Data.FirstOrDefault(x => x.Id.Equals(item.Id));

            book.Title = item.Title;
            book.Author = item.Author;
            book.Description = item.Description;
            book.IsForSale = item.IsForSale;
            
            // if book is published set current date, if unpublished leave previous publish date
            if (!book.IsPublished && item.IsPublished)
            {
                book.Published = DateTime.UtcNow;
            }

            book.IsPublished = item.IsPublished;

            result.Data = _bookRepo.Update(book);
            result.Success = true;

            return result;
        }
    }
}
