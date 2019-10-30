using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.Requests.Author;

namespace BooksReader.Core.Services
{
    public interface IBookChapterService
    {
        IOperationResult<BookChapter> Add(Guid bookId, BookChapterRequest data, BrUser user);
        IOperationResult<BookChapter> Update(Guid bookId, BookChapterRequest data, BrUser user);
        IOperationResult<BookChapter> Delete(Guid bookId, Guid chapterId, BrUser user);

        IOperationResult Reorder(Guid bookId, IEnumerable<BookChapterReorderRequest> order, BrUser user);

    }
}
