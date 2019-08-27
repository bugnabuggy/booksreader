using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastructure;
using BooksReader.Core.Models;
using BooksReader.Core.Models.Requests;

namespace BooksReader.Core.Services
{
	public interface IBooksService : ICRUDService<Book>, IValidator<Book>, IValidator<BookEditFullRequest>
    {
        IQueryable<Book> GetByOwnerId(Guid ownerId);

        IOperationResult<BookEditInfo> GetFull(Guid id);
        IOperationResult<BookEditFullRequest> SaveFull(BookEditFullRequest bookInfo);
    }
}
