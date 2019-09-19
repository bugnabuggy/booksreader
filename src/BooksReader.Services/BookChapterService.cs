using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastructure;
using BooksReader.Core.Models;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Services;
using BooksReader.Dictionaries;
using Microsoft.EntityFrameworkCore;
using MessageStrings = BooksReader.Dictionaries.Messages.MessageStrings;

namespace BooksReader.Services
{
    public class BookChapterService: IBookChapterService
    {
        private readonly IRepository<BookChapter> _chaptersRepo;
        private readonly IRepository<Book> _booksRepo;

        private readonly IEnumerable<Expression<Func<BookChapter, string>>> _validations =
            new List<Expression<Func<BookChapter, string>>>()
            {
                {x => string.IsNullOrWhiteSpace(x.Title)
                    ? "VALIDATION.BookChapters.NoTitle"
                    : ""
                },
                {x => Guid.Empty.Equals(x.BookId)
                    ? "VALIDATION.CalendarEvent.NoBook"
                    : ""
                },
                //{x => string.IsNullOrWhiteSpace(x.Description)
                //    ? "VALIDATION.CalendarEvent.NoDescription"
                //    : ""
                //}
            };

        public BookChapterService(
            IRepository<BookChapter> chaptersRepo,
            IRepository<Book> booksRepo)
        {
            _chaptersRepo = chaptersRepo;
            _booksRepo = booksRepo;
        }


        public IEnumerable<string> Validate(BookChapter item)
        {
            var result = new List<string>();

            foreach (var validation in _validations)
            {
                // get message
                var validMsg = validation
                    .Compile()
                    .Invoke(item);

                // if message not empty add it to messages
                if (!string.IsNullOrWhiteSpace(validMsg))
                {
                    result.Add(validMsg);
                }
            }

            return result;
        }


        public BookChapter Get(Guid id)
        {
            return _chaptersRepo.Data.FirstOrDefault(x => x.Id.Equals(id));
        }

        public IQueryable<BookChapter> Get()
        {
            return _chaptersRepo.Data;
        }

        public IQueryable<BookChapter> GetByBook(Guid bookId)
        {
            return _chaptersRepo.Data.Where(x => x.BookId.Equals(bookId));
        }

        public IOperationResult<BookChapter> AddOrUpdate(Guid bookId, BookChapterRequest chapterInfo)
        {
            IOperationResult<BookChapter> result = new OperationResult<BookChapter>();

            var bookChapter = _chaptersRepo.Data.FirstOrDefault(x => x.Id.Equals(chapterInfo.Id));

            // if there is a existing chapter use it to update 
            bookChapter = bookChapter != null
                    ? bookChapter
                    : new BookChapter()
                    {
                        Id = chapterInfo.Id ?? Guid.Empty,
                        Title =  chapterInfo.Title,
                        Description = chapterInfo.Description,
                        BookId = bookId,
                    };

            var validations = Validate(bookChapter).ToList();

            var book = _booksRepo.Data.FirstOrDefault(x => x.Id.Equals(bookId));
            if (book == null)
            {
                validations.Add(MessageStrings.BookChapterMessages.BookDoesNotExists);
            }

            if (validations.Any())
            {
                result.Success = false;
                result.Messages = validations;
            }
            else
            {
                // all chapters should be owned by the same user as a book
                bookChapter.OwnerId = book.OwnerId;

                result = bookChapter.Id.Equals(Guid.Empty)
                    ? Add(bookChapter)
                    : Edit(bookChapter);
            }

            return result;
        }

     
        public IOperationResult<IEnumerable<BookChapterReorderRequest>> Reorder(Guid bookId, IEnumerable<BookChapterReorderRequest> order)
        {
            var result = new OperationResult<IEnumerable<BookChapterReorderRequest>>(true);
            var data = new List<BookChapterReorderRequest>();

            var chapters = _chaptersRepo.Data.Where(x => x.BookId.Equals(bookId)).ToList();

            foreach (var chapterOrder in order)
            {
                var chapter = chapters.FirstOrDefault(x => x.Id.Equals(chapterOrder.Id));
                if (chapter != null)
                {
                    chapter.Number = chapterOrder.Number;
                }
                else
                {
                    data.Add(chapterOrder);
                }
            }

            // save changes
            _chaptersRepo.Update(chapters);

            if (data.Any())
            {
                result.Success = false;
                result.Messages.Add(MessageStrings.BookChapterMessages.SomeChaptersNotFoundDuringReorder);
            }

            return result;
        }


        public IOperationResult<BookChapter> Add(BookChapter item)
        {
            var result = new OperationResult<BookChapter>()
            {
                Data = item
            };

            var validations = Validate(item).ToList();

            if (validations.Any())
            {
                result.Success = false;
                result.Messages = validations;
            }
            else
            {
                item.Created = DateTime.UtcNow;
                var newItem = _chaptersRepo.Add(item);
                result.Data = newItem;
                result.Success = true;
            }

            return result;
        }

        public IOperationResult<BookChapter> Edit(BookChapter item)
        {

            var result = new OperationResult<BookChapter>()
            {
                Data = item
            };

            var validations = Validate(item).ToList();

            if (validations.Any())
            {
                result.Success = false;
                result.Messages = validations;
            }
            else
            {
                var newItem = _chaptersRepo.Update(item);
                result.Data = newItem;
                result.Success = true;
            }

            return result;
        }

        public IOperationResult<BookChapter> EditContent(Guid bookId, BookChapter chapterInfo)
        {
            var result = new OperationResult<BookChapter>(chapterInfo);

            try
            {
                var existinChapter = _chaptersRepo.Data.FirstOrDefault(x => x.Id.Equals(chapterInfo.Id));
                chapterInfo.Version = ++existinChapter.Version;
                _chaptersRepo.Update(chapterInfo);
                result.Success = true;
            }
            catch (Exception exp)
            {
                result.Messages.Add(exp.Message);
            }

            return result;
        }


        public IOperationResult<BookChapter> Delete(Guid id)
        {
            var result =  new OperationResult<BookChapter>();

            var chapter = _chaptersRepo.Data.FirstOrDefault(x => x.Id.Equals(id));

            if (chapter == null)
            {
                result.Messages.Add(MessageStrings.NotFound);
            }
            else
            {
                var deletedItem = _chaptersRepo.Delete(chapter);
                result.Data = deletedItem;
                result.Success = true;
            }

            return result;
        }

        public Task<BookChapter> GetAsync(Guid id)
        {
            return _chaptersRepo.Data.FirstOrDefaultAsync(x => x.Id.Equals(id));
        }

        public async Task<IEnumerable<BookChapter>> GetAsync()
        {
            return await _chaptersRepo.Data.ToListAsync();
        }

        public async Task<IOperationResult<BookChapter>> AddAsync(BookChapter item)
        {
            var result = new OperationResult<BookChapter>()
            {
                Data = item
            };

            var validations = Validate(item).ToList();

            if (validations.Any())
            {
                result.Success = false;
                result.Messages = validations;
            }
            else
            {
                var newItem = await  _chaptersRepo.AddAsync(item);
                result.Data = newItem;
                result.Success = true;
            }

            return result;
        }
    }
}
