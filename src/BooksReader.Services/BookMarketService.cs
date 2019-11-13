using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Enums;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.DTO.Public;
using BooksReader.Core.Models.Requests.Public;
using BooksReader.Core.Services;
using BooksReader.Dictionaries.Messages;
using BooksReader.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BooksReader.Services
{
    public class BookMarketService: IBookMarketService
    {
        private readonly IRepository<Book> _booksRepo;
        private readonly IRepository<AuthorProfile> _authorsRepo;
        private readonly IRepository<BookSubscription> _subscriptionsRepo;

        public BookMarketService(
            IRepository<Book> booksRepo,
            IRepository<AuthorProfile> authorsRepo,
            IRepository<BookSubscription> subscriptionsRepo
            )
        {
            _booksRepo = booksRepo;
            _authorsRepo = authorsRepo;
            _subscriptionsRepo = subscriptionsRepo;
        }

        public IWebResult<IEnumerable<BookMarketDto>> GetBooks(BookMarketFilters filters, BrUser user)
        {
            user = user ?? new BrUser();

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


            query = PaginationHelper.GetPaged(query, filters, out int totalRecords);


            //TODO: create expression delegate to handle projection 
            var data = query
                .Include(x=>x.Prices)
                .Include(x=>x.Subscriptions)
                .Join(_authorsRepo.Data, x=>x.OwnerId, y => y.UserId ,(x, y)=> new { Book = x, Author = y})
                .Select(x => new BookMarketDto()
            {
                Title = x.Book.Title,
                BookId = x.Book.Id,
                SemanticUrl = x.Book.SemanticUrl,
                Published = x.Book.Published,
                IsForSale = x.Book.IsForSale,
                Author = x.Book.Author,
                AuthorId = x.Author.Id,
                AuthorSemanticUrl =  x.Author.SemanticUrl,
                Picture = x.Book.Picture,
                SubscriptionDurationDays = x.Book.SubscriptionDurationDays,
                BookPrices = x.Book.Prices,
                Subscription = x.Book.Subscriptions.All(b => b.UserId != user.Id) 
                    ? SubscriptionStatus.None
                    : x.Book.Subscriptions.FirstOrDefault(y =>y.UserId == user.Id).EndDate > DateTime.UtcNow
                      ||
                      x.Book.Subscriptions.FirstOrDefault(y => y.UserId == user.Id).EndDate == null
                      ? SubscriptionStatus.Active
                      : SubscriptionStatus.Ended
                });

            var result = new WebResult<IEnumerable<BookMarketDto>>()
            {
                Data = data,
                Total = totalRecords,
                PageNumber =  filters.PageNumber,
                PageSize =  filters.PageSize,
                Success = true
            };

            return result;
        }

        public BookMarketDto GetBook(Guid id, BrUser user)
        {
            user = user ?? new BrUser();

            var query = _booksRepo.Data.AsNoTracking().Where(x=>x.Id == id);

            var data = query
                .Include(x => x.Prices)
                .Include(x=>x.Subscriptions)
                .Join(_authorsRepo.Data, x => x.OwnerId, y => y.UserId, (x, y) => new { Book = x, Author = y })
                .Select(x => new BookMarketDto()
                {
                    Title = x.Book.Title,
                    BookId = x.Book.Id,
                    SemanticUrl = x.Book.SemanticUrl,
                    Published = x.Book.Published,
                    IsForSale = x.Book.IsForSale,
                    Author = x.Book.Author,
                    AuthorId = x.Author.Id,
                    AuthorSemanticUrl = x.Author.SemanticUrl,
                    Picture = x.Book.Picture,
                    SubscriptionDurationDays = x.Book.SubscriptionDurationDays,
                    BookPrices = x.Book.Prices,
                    Subscription = x.Book.Subscriptions.All(b => b.UserId != user.Id)
                        ? SubscriptionStatus.None
                        : x.Book.Subscriptions.FirstOrDefault(y => y.UserId == user.Id).EndDate > DateTime.UtcNow || 
                        x.Book.Subscriptions.FirstOrDefault(y => y.UserId == user.Id).EndDate == null 
                            ? SubscriptionStatus.Active
                            : SubscriptionStatus.Ended
                }).FirstOrDefault();

            return data;
        }

        public IOperationResult<BookSubscription> Add(Guid bookId, BrUser user)
        {
            var result = new OperationResult<BookSubscription>();

            if (user == null)
            {
                result.Messages.Add(MessageStrings.SubscriptionMessages.UserCantBeEmpty);
                return result;
            }

            var existingSub = _subscriptionsRepo.Data
                .FirstOrDefault(x => x.BookId == bookId && x.UserId == user.Id);

            if (existingSub != null)
            {
                result.Messages.Add(MessageStrings.SubscriptionMessages.SubscriptionAlreadyExists);
                result.Data = existingSub;
                return result;
            }
            
            var book = _booksRepo.Data
                .AsNoTracking()
                .FirstOrDefault(x => x.Id == bookId);


            // TODO: implement payment checks for books for sale
            DateTime? subscriptionEndDate = !book.IsForSale
                ? null
                : (DateTime?)DateTime.UtcNow.AddDays(book.SubscriptionDurationDays);

            var subscription = new BookSubscription()
            {
                BookId = bookId,
                StartDate = DateTime.UtcNow,
                EndDate =  subscriptionEndDate,
                UserId = user.Id,
            };

            _subscriptionsRepo.Add(subscription);

            result.Data = subscription;
            result.Success = true;

            return result;
        }
    }
}
