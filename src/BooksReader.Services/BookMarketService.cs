using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.DTO.Public;
using BooksReader.Core.Models.Requests.Public;
using BooksReader.Core.Services;
using BooksReader.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BooksReader.Services
{
    public class BookMarketService: IBookMarketService
    {
        private readonly IRepository<Book> _booksRepo;
        private readonly IRepository<BookSubscription> _subscriptionsRepo;

        public BookMarketService(
            IRepository<Book> booksRepo,
            IRepository<BookSubscription> subscriptionsRepo
            )
        {
            _booksRepo = booksRepo;
            _subscriptionsRepo = subscriptionsRepo;
        }

        public IWebResult<IEnumerable<BookMarketDto>> GetBooks(BookMarketFilters filters, BrUser user)
        {
            

            var query = _booksRepo.Data.AsNoTracking();
            
            // only published and verified
            query = query.Where(x => x.IsPublished && x.Verified);

            query = string.IsNullOrWhiteSpace(filters.Title)
                ? query
                : query.Where(x=>x.Title.ToLower().Contains(filters.Title.ToLower()));

            query = string.IsNullOrWhiteSpace(filters.Author)
                ? query
                : query.Where(x => x.Author.ToLower().Contains(filters.Author.ToLower()));

            query = !filters.IsForSale.HasValue
                ? query
                : query.Where(x=>x.IsForSale == filters.IsForSale.Value);


            query = PaginationHelper.GetPaged(query, filters, out int toatRecords);

            var data = query.Include(x=>x.Prices).Select(x => new BookMarketDto()
            {
                Title = x.Title,
                BookId = x.Id,
                Published = x.Published,
                IsForSale = x.IsForSale,
                Author = x.Author,
                Picture = x.Picture,
                BookPrices = x.Prices

            });

            var result = new WebResult<IEnumerable<BookMarketDto>>()
            {
                Data = data,
                Total = toatRecords,
                PageNumber =  filters.PageNumber,
                PageSize =  filters.PageSize,
                Success = true
            };

            return result;
        }
    }
}
