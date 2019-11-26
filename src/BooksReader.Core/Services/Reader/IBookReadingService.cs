using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.DTO.Reader;

namespace BooksReader.Core.Services.Reader
{
    public interface IBookReadingService
    {
        IOperationResult<BookReadingDto> GetBookForReading(Guid bookId, string connectionId, BrUser user);
        IOperationResult<BookChapter> GetChapterContent(Guid bookId, Guid chapterId, BrUser user);
    }
}
