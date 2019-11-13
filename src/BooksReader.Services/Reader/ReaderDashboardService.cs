using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Enums;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.DTO.Public;
using BooksReader.Core.Models.DTO.Reader;
using BooksReader.Core.Models.Requests.Reader;
using BooksReader.Core.Services.Reader;
using BooksReader.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BooksReader.Services.Reader
{
    public class ReaderDashboardService: IReaderDashboardService
    {
        private readonly IRepository<Book> _booksRepo;
        private readonly IRepository<BookSubscription> _subscriptionsRepo;

        public ReaderDashboardService(
            IRepository<Book> booksRepo,
            IRepository<BookSubscription> subsRepo
            )
        {
            _booksRepo = booksRepo;
            _subscriptionsRepo = subsRepo;
        }

        public IWebResult<IEnumerable<ReaderDashboardBookDto>> GetReaderBooks(ReaderDashboardFilters filters, BrUser user)
        {
            var query = _subscriptionsRepo.Data.AsNoTracking();

            query = filters.UserId.HasValue 
                ? query.Where(x=>x.UserId == filters.UserId.Value)
                : query;

            query = PaginationHelper.GetPaged(query, filters, out var totalRecords);

            var data = query.Include(x=>x.Book).Select(x => new ReaderDashboardBookDto()
            {
                Book = new BookMarketDto()
                {
                    Title =  x.Book.Title,
                    Published =  x.Book.Published,
                    IsForSale =  x.Book.IsForSale,
                    Picture = x.Book.Picture,
                    SemanticUrl =  x.Book.SemanticUrl,
                    Subscription =  x.EndDate.HasValue && x.EndDate.Value > DateTime.UtcNow
                            ? SubscriptionStatus.Active
                            : SubscriptionStatus.Ended,
                    Author = x.Book.Author,
                    
                },
                Subscription = new BookSubscriptionDto()
                {
                    BookId = x.BookId,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    SubscriptionId = x.Id
                }
            });

            return new WebResult<IEnumerable<ReaderDashboardBookDto>>()
            {
                Data = data,
                Success = true,
                Total = totalRecords,
                PageNumber = filters.PageNumber,
                PageSize = filters.PageSize
            };
        }
    }
}
