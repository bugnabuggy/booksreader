using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Enums;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.DTO.Public;
using BooksReader.Core.Models.DTO.Reader;
using BooksReader.Core.Models.Requests.Reader;
using BooksReader.Core.Services.Reader;
using BooksReader.Dictionaries.Messages;
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
                Book = new GeneralBook()
                {
                    BookId = x.BookId,
                    Title =  x.Book.Title,
                    IsForSale =  x.Book.IsForSale,
                    Picture = x.Book.Picture,
                    Author = x.Book.Author
                },
                Subscription = new BookSubscriptionDto()
                {
                    BookId = x.BookId,
                    StartDate = x.StartDate,
                    EndDate = x.EndDate,
                    SubscriptionId = x.Id,
                    Status = x.EndDate.HasValue && x.EndDate.Value > DateTimeOffset.UtcNow
                        ? SubscriptionStatus.Active
                        : SubscriptionStatus.Ended,
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

        public IOperationResult<object> RemoveSubscription(Guid bookId, BrUser user)
        {
            var result = new OperationResult();

            var sub = _subscriptionsRepo.Data
                .FirstOrDefault(x => x.BookId == bookId && x.UserId == user.Id);

            if (sub == null)
            {
                result.Messages.Add(MessageStrings.ReaderMessages.NoSubscriptionForUser);
                return result;
            }

            // todo: prevent removing not ended subscriptions

            _subscriptionsRepo.Delete(sub);
            result.Data = sub;
            result.Success = true;

            return result;
        }
    }
}
