using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BooksReader.Core;
using BooksReader.Core.Entities;
using BooksReader.Core.Enums;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.DTO.Author;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Models.Requests.Author;
using BooksReader.Core.Services;
using BooksReader.Dictionaries.Messages;
using BooksReader.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BooksReader.Services
{
    public class BookEditingService: IBookEditingService
    {
        private readonly IRepository<Book> _booksRepo;
        private readonly IRepository<PublicPage> _pagesRepo;
        private readonly IRepository<BookPrice> _pricessRepo;
        private readonly IRepository<BookChapter> _chaptersRepo;

        private readonly IMapper _mapper;

        private IEnumerable<Func<Book, BookEditingService, BrUser, string>> _validations =
            new List<Func<Book, BookEditingService, BrUser, string>>()
            {
                // title should not be empty 
                (book, svc, user) =>
                {
                    var msg = string.IsNullOrWhiteSpace(book.Title)
                        ? MessageStrings.BooksMessages.BookTitleShouldNotBeEmpty
                        : "";

                    return msg;
                },

                // if book for sale it should have prices
                (book, svc, user) =>
                {
                    if (!book.IsForSale) return "";

                    var hasPrices = svc._pricessRepo.Data
                        .AsNoTracking()
                        .Any(x => x.BookId.Equals(book.Id));

                    var msg = hasPrices
                        ? ""
                        : MessageStrings.BooksMessages.BookForSaleMustHavePrices;

                    return msg;

                }
            };
        
        public BookEditingService(
                IRepository<Book> booksRepo,
                IRepository<PublicPage>pagesRepo,
                IRepository<BookPrice> pricessRepo,
                IRepository<BookChapter> chaptersRepo,
                IMapper mapper
            )
        {
            _booksRepo = booksRepo;
            _mapper = mapper;
            _pagesRepo = pagesRepo;
            _pricessRepo = pricessRepo;
            _chaptersRepo = chaptersRepo;
        }

        public IWebResult<IEnumerable<Book>> GetBooks(StandardBooksFilters filters)
        {
            var books = _booksRepo.Data.AsNoTracking();
                
            books = filters.UserId.HasValue 
                ? books.Where(x=>x.OwnerId.Equals(filters.UserId))
                : books;

            books = string.IsNullOrWhiteSpace(filters.Title)
                ? books
                : books.Where(x => x.Title.ToLower().Contains(filters.Title.ToLower()));

            books = string.IsNullOrWhiteSpace(filters.Description)
                ? books
                : books.Where(x=>x.Description.ToLower().Contains(filters.Description.ToLower()));

            books = PaginationHelper.GetPaged(books, filters, out var total);


            var result = new WebResult<IEnumerable<Book>>()
            {
                Data = books,
                Success = true,

                Total = total,
                PageNumber = filters.PageNumber ?? 1,
                PageSize =  filters.PageSize ?? Constants.DefaultPageSize
            };

            return result;
        }

        public IEnumerable<string> Validate(Book book, BrUser user)
        {
            var validations = _validations
                .Select(x => x(book, this, user))
                .Where(x => !string.IsNullOrWhiteSpace(x));

            return validations;
        }

        public IOperationResult<Book> Add(BookBasicInfoRequest basicBook, BrUser user)
        {
            var result = new OperationResult<Book>();

            var book = new Book()
                {
                    Title = basicBook.Title,
                    Author = basicBook.Author,
                    Picture = basicBook.Picture,
                    
                    Created = DateTime.UtcNow,
                    OwnerId =  user.Id
                };

            var validations = Validate(book, user).ToList();
            if (validations.Any())
            {
                result.Messages = validations;
                return result;
            }

            _booksRepo.Add(book);
            
            result.Data = book;
            result.Success = true;

            return result;
        }

        public IOperationResult<Book> Edit(BookBasicInfoRequest basicBook, BrUser user)
        {
            var result = new OperationResult<Book>();

            var book = _mapper.Map<Book>(basicBook);

            var validations = Validate(book, user).ToList();
            if (validations.Any())
            {
                result.Messages = validations;
                return result;
            }

            var existingBook = _booksRepo.Data.FirstOrDefault(x => x.Id == basicBook.Id);

            // update basic info
            existingBook.Title = basicBook.Title;
            existingBook.Author = basicBook.Author;
            existingBook.Picture = basicBook.Picture;

            _booksRepo.Update(existingBook);

            result.Data = existingBook;
            result.Success = true;

            return result;
        }

        public IOperationResult<Book> Delete(Guid id, BrUser user)
        {
            var book = _booksRepo.Data.FirstOrDefault(x => x.Id == id);

            // TODO: validate here about subscriptions and so on.

            _booksRepo.Delete(book);
            
            return new OperationResult<Book>()
            {
                Data = book,
                Success = true
            };
        }

        public IOperationResult<BookFullEditInfoDto> Get(Guid bookId)
        {
            var book = _booksRepo.Data.AsNoTracking()
                .FirstOrDefault(x=>x.Id.Equals(bookId));

            var prices = _pricessRepo.Data.AsNoTracking()
                .Where(x => x.BookId.Equals(bookId));

            var page = _pagesRepo.Data.AsNoTracking()
                .FirstOrDefault(x => x.SubjectId.Equals(bookId) && x.PageType == PublicPageType.BookPage);

            var chapters = _chaptersRepo.Data.AsNoTracking()
                .Where(x => x.BookId.Equals(bookId))
                .OrderBy(x=>x.Number);

            var data = new BookFullEditInfoDto()
            {
                Book = book,
                Prices = prices,
                Page = page,
                Chapters = chapters
            };

            var result = new OperationResult<BookFullEditInfoDto>()
            {
                Data = data,
                Success = true
            };
            
            return result;
        }

        public IOperationResult<Book> EditFull(BookEditRequest bookData, BrUser user)
        {
            var result = new OperationResult<Book>();
            var book = _mapper.Map<Book>(bookData);

            var validations = Validate(book, user).ToList();
            if (validations.Any())
            {
                result.Messages = validations;
                return result;
            }

            var existingBook = _booksRepo.Data.FirstOrDefault(x => x.Id == bookData.Id);

            // update basic info
            existingBook.Title = bookData.Title;
            existingBook.Author = bookData.Author;
            existingBook.Picture = bookData.Picture;
            existingBook.Description = bookData.Description;
            existingBook.IsForSale = bookData.IsForSale;
            existingBook.SubscriptionDurationDays = bookData.SubscriptionDurationDays;
            existingBook.IsPublished = book.IsPublished;
            existingBook.Published = book.IsPublished
                ? DateTime.UtcNow
                : (DateTime?)null;

            existingBook.Verified = false;

            _booksRepo.Update(existingBook);

            result.Data = existingBook;
            result.Success = true;

            return result;
        }
    }
}
