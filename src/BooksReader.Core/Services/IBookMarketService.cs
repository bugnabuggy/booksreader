using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.DTO.Public;
using BooksReader.Core.Models.Requests.Public;

namespace BooksReader.Core.Services
{
    public interface IBookMarketService
    {
        IWebResult<IEnumerable<BookMarketDto>> GetBooks(BookMarketFilters filters, BrUser user);
    }
}
