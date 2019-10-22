using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BooksReader.Core;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
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
            };
        
        public BookEditingService(
            IRepository<Book> booksRepo,
            IMapper mapper
            )
        {
            _booksRepo = booksRepo;
            _mapper = mapper;
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
            
            var book = _booksRepo.Data.FirstOrDefault(x => x.Id == basicBook.Id);

            var validations = Validate(book, user).ToList();
            if (validations.Any())
            {
                result.Messages = validations;
                return result;
            }
            
            book.Title = basicBook.Title;
            book.Author = basicBook.Author;
            book.Picture = basicBook.Picture;

            _booksRepo.Update(book);

            result.Data = book;
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
    }
}
