using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastructure;
using BooksReader.Core.Models;

namespace BooksReader.Core.Services
{
    public interface IBookPriceService: ICRUDOperatonService<BookPrice>, IValidator<BookPrice>
    {
        IOperationResult<IEnumerable<BookPrice>> SetPrices(IEnumerable<BookPrice> prices);
    }
}
