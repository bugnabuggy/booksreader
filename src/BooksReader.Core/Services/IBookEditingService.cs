using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Models.Requests.Author;

namespace BooksReader.Core.Services
{
    public interface IBookEditingService
    {
        IWebResult<IEnumerable<Book>> GetBooks(StandardBooksFilters filters);
        IOperationResult<Book> Add(BookBasicInfoRequest book, BrUser user);
        IOperationResult<Book> Edit(BookBasicInfoRequest book, BrUser user);
        IOperationResult<Book> Delete(Guid id, BrUser user);
    }
}
