using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.Requests.Author;

namespace BooksReader.Core.Services
{
    public interface IBookPricesService
    {
        IOperationResult<IEnumerable<BookPrice>> GetPrices(Guid bookId);
        
        IOperationResult<BookPrice> Add(BookPriceRequest price, BrUser user);
        IOperationResult<BookPrice> Edit(BookPriceRequest price, BrUser user);
        IOperationResult<BookPrice> Delete(Guid priceId, BrUser user);
    }
}
