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
    public interface IBookPriceService: ICRUDOperatonService<BookPrice>, IValidator<BookPrice>
    {
        IOperationResult<IEnumerable<BookPrice>> SetPrices(IEnumerable<BookPrice> prices);
        IOperationResult<IEnumerable<BookPrice>> SetPrices(IEnumerable<BookPricesRequest> prices, Guid bookId);

        IQueryable<BookPrice> GetByBookId(Guid bookId);
    }
}
