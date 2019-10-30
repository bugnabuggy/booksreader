using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.Requests.Author;
using BooksReader.Core.Services;
using BooksReader.Dictionaries.Messages;
using Microsoft.EntityFrameworkCore;

namespace BooksReader.Services
{
    public class BookChapterService :  IBookChapterService
    {
        private IEnumerable<Func<BookChapter, BookChapterService, BrUser, string>> _validatios = 
            new List<Func<BookChapter, BookChapterService, BrUser, string>>()
            {
                // chapter should has a title
                (chapter, svc, user) =>
                {
                    var msg = string.IsNullOrWhiteSpace(chapter.Title)
                        ? MessageStrings.BookChapterMessages.TitleShouldNotBeEmpty
                        : "";
                    return msg;
                },

                // book must not be empty
                (chapter, svc, user) =>
                {
                    var bookExists = svc._booksRepo.Data.AsNoTracking()
                        .Any(x => x.Id == chapter.BookId);

                    var msg = bookExists 
                        ? ""
                        : MessageStrings.BookChapterMessages.BookDoesNotExists;

                    return msg;
                }

            };
        
        
        private readonly IRepository<BookChapter> _chaptersRepo;
        private readonly IRepository<Book> _booksRepo;
        private readonly IMapper _mapper;

        public BookChapterService(
            IRepository<Book> booksRepo,
            IRepository<BookChapter> chaptersRepo,
            IMapper mapper)
        {
            _chaptersRepo = chaptersRepo;
            _booksRepo = booksRepo;
            _mapper = mapper;
        }

        public IOperationResult<BookChapter> Add(Guid bookId, BookChapterRequest data, BrUser user)
        {
            var result = new OperationResult<BookChapter>();

            var bookChapter = _mapper.Map<BookChapter>(data);
            bookChapter.BookId = bookId;

            var validations = Validate(bookChapter, user).ToList();

            if (validations.Any())
            {
                result.Messages = validations;
                return result;
            }

            var chapters = _chaptersRepo.Data.AsNoTracking()
                                .Where(x => x.BookId == bookId);

            bookChapter.Created = DateTime.UtcNow;
            bookChapter.Version = 0;
            bookChapter.OwnerId = user.Id;
            bookChapter.Number = chapters.Any() 
                ? chapters.Max(x => x.Number) + 1
                : 0;

            _chaptersRepo.Add(bookChapter);

            result.Data = bookChapter;
            result.Success = true;

            return result;
        }

        public IOperationResult<BookChapter> Update(Guid bookId, BookChapterRequest data, BrUser user)
        {
            var result = new OperationResult<BookChapter>();
            
            var bookChapter = _mapper.Map<BookChapter>(data);
            bookChapter.BookId = bookId;

            var validations = Validate(bookChapter, user).ToList();

            if (validations.Any())
            {
                result.Messages = validations;
                return result;
            }

            var existingChapter = _chaptersRepo.Data.FirstOrDefault(x => x.Id.Equals(data.Id));

            existingChapter.Content = data.Content;
            existingChapter.Title = data.Title;
            existingChapter.IsPublished = data.IsPublished;
            existingChapter.Description = data.Description;
            // do staff for publishing and verification checking

            _chaptersRepo.Update(existingChapter);

            result.Data = existingChapter;
            result.Success = true;

            return result;
        }

        public IOperationResult<BookChapter> Delete(Guid bookId, Guid chapterId, BrUser user)
        {
            var result = new OperationResult<BookChapter>();

            var chapter = _chaptersRepo.Data.FirstOrDefault(x => x.Id.Equals(chapterId));
            
            _chaptersRepo.Delete(chapter);

            result.Data = chapter;
            result.Success = true;

            return result;
        }

        public IOperationResult Reorder(Guid bookId, IEnumerable<BookChapterReorderRequest> order, BrUser user)
        {
            var result = new OperationResult<IEnumerable<BookChapterReorderRequest>>(true);
            var notFoundChapters = new List<BookChapterReorderRequest>();

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
                    notFoundChapters.Add(chapterOrder);
                }
            }

            // save changes
            _chaptersRepo.Update(chapters);

            if (notFoundChapters.Any())
            {
                result.Success = false;
                result.Messages.Add(MessageStrings.BookChapterMessages.SomeChaptersNotFoundDuringReorder);
            }

            return result;
        }


        public IEnumerable<string> Validate(BookChapter chapter, BrUser user)
        {
            var validations = _validatios.Select(x => x(chapter, this, user));

            return validations.Where(x => !string.IsNullOrWhiteSpace(x));
        }
    }
}
