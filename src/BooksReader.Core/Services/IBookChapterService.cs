using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Models;
using BooksReader.Core.Models.Requests;

namespace BooksReader.Core.Services
{
    public interface IBookChapterService : ICRUDOperatonService<BookChapter>
    {
        IQueryable<BookChapter> GetByBook(Guid bookId);

        IOperationResult<BookChapter> AddOrUpdate(Guid bookId, BookChapterRequest chapterInfo);
    }
}
