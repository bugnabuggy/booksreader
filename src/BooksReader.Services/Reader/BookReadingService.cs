using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using AutoMapper.QueryableExtensions;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.DTO.Reader;
using BooksReader.Core.Services.Reader;
using BooksReader.Dictionaries.Messages;
using Microsoft.EntityFrameworkCore;

namespace BooksReader.Services.Reader
{
    public class BookReadingService : IBookReadingService
    {
        private readonly IRepository<BookSubscription> _subscriptionsRepo;
        private readonly IRepository<Book> _booksRepo;
        private readonly IRepository<BookChapter> _chaptersRepo;
        private readonly IMapper _mapper;

        public BookReadingService(
            IRepository<BookSubscription> subscriptionsRepo,
            IRepository<Book> booksRepo,
            IRepository<BookChapter> chaptersRepo,
            IMapper mapper
            )
        {
            _booksRepo = booksRepo;
            _chaptersRepo = chaptersRepo;
            _subscriptionsRepo = subscriptionsRepo;
            _mapper = mapper;
        }

        public IOperationResult<BookReadingDto> GetBookForReading(Guid bookId, string connectionId, BrUser user)
        {
            var result = new OperationResult<BookReadingDto>();
            var validations = CanReadBook(bookId, user);

            if (validations.Any())
            {
                result.Messages = validations;
            } 
            else
            {
                var book = _booksRepo.Data.AsNoTracking()
                    .FirstOrDefault(x => x.Id == bookId);

                var chapters = _chaptersRepo.Data.AsNoTracking()
                    .Where(x => x.BookId == bookId)
                    .Where(x => x.IsPublished)
                    .OrderBy(x => x.Number);

                var data = new BookReadingDto()
                {
                    Book = _mapper.Map<GeneralBook>(book),
                    Chapters = chapters.Select(x=> _mapper.Map<ChapterReadingDto>(x)), 
                    SessionId = connectionId
                };

                result.Data = data;
                result.Success = true;
            }

            return result;
        }

        public IOperationResult<BookChapter> GetChapterContent(Guid bookId, Guid chapterId, BrUser user)
        {
            var result = new OperationResult<BookChapter>();
            var validations = CanReadChapter(bookId, chapterId, user);
            if (validations.Any())
            {
                result.Messages = validations;
            }
            else
            {
                var chapter = _chaptersRepo.Data.AsNoTracking()
                    .FirstOrDefault(x => x.Id == chapterId);
                
                result.Success = true;
                result.Data = chapter;
            }

            return result;
        }


        private IList<string> CanReadBook(Guid bookId, BrUser user)
        {
            // user can read the book if he has an subscription
            // and subscription end date is null or greater than present moment

            var result = new List<string>();

            // get subscription for the book and the user
            var sub = _subscriptionsRepo.Data.AsNoTracking()
                .FirstOrDefault(x => x.BookId.Equals(bookId) && x.UserId.Equals(user.Id));

            if (sub == null)
            {
                result.Add(MessageStrings.BookReadingMessages.NoSubscription);
            }
            else
            {
                if (sub.EndDate.HasValue)
                {
                    if (sub.EndDate.Value.UtcDateTime < DateTime.UtcNow)
                    {
                        result.Add(MessageStrings.BookReadingMessages.SubscriptionEnded);
                    }
                }
            }

            return result;
        }

        private IList<string> CanReadChapter(Guid bookId, Guid chapterId, BrUser user)
        {
            // for now only check if a user can read the book
            return CanReadBook(bookId, user);
        }
    }
}
