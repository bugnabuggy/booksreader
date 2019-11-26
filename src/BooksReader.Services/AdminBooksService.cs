using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.DTO.Admin;
using BooksReader.Core.Models.Requests.Admin;
using BooksReader.Core.Services;
using BooksReader.Dictionaries.Messages;
using BooksReader.Utilities;
using Microsoft.EntityFrameworkCore;

namespace BooksReader.Services
{
    public class AdminBooksService : IAdminBooksService
    {
        private readonly IRepository<Book> _booksRepo;
        private readonly IRepository<BrUser> _usersRepo;
        private readonly IMapper _mapper;

        public AdminBooksService(
            IRepository<Book> booksRepo,
            IRepository<BrUser> usersRepo,
            IMapper mapper
            )
        {
            _booksRepo = booksRepo;
            _usersRepo = usersRepo;
            _mapper = mapper;
        }

        public IWebResult<IEnumerable<AdminBookDto>> GetBooks(AdminAllBooksFilter filters)
        {
            var query = _booksRepo.Data.AsNoTracking();

            var users = _usersRepo.Data.AsNoTracking();

            if (!string.IsNullOrWhiteSpace(filters.Username))
            {
                users = users
                    .Where(x => x.Name.ToLower()
                    .Contains(filters.Username.ToLower()));
            }

            query = string.IsNullOrWhiteSpace(filters.Title)
                ? query
                : query.Where(x => x.Title.Contains(filters.Title));

            query = !filters.IsPublished.HasValue
                ? query
                : query.Where(x => x.IsPublished == filters.IsPublished.Value);

            query = !filters.Verified.HasValue
                ? query
                : query.Where(x => x.Verified == filters.Verified.Value);



            if (!string.IsNullOrWhiteSpace(filters.PublishedFrom))
            {
                var publishedFrom = DateTimeOffset.Parse(filters.PublishedFrom);
                query = query.Where(x => x.Published >= publishedFrom);
            }

            if (!string.IsNullOrWhiteSpace(filters.PublishedTo))
            {
                var publishedTo = DateTimeOffset.Parse(filters.PublishedTo);
                query = query.Where(x => x.Published <= publishedTo);
            }

            if (!string.IsNullOrWhiteSpace(filters.CreatedFrom))
            {
                var createdFrom = DateTimeOffset.Parse(filters.CreatedFrom);
                query = query.Where(x => x.Published >= createdFrom);
            }

            if (!string.IsNullOrWhiteSpace(filters.CreatedTo))
            {
                var createdTo = DateTimeOffset.Parse(filters.CreatedTo);
                query = query.Where(x => x.Published <= createdTo);
            }

            query = PaginationHelper.GetPaged(query, filters, out int totalRecords);

            var data = query.Join(users, x => x.OwnerId, y => y.Id, (x, y) => new
                AdminBookDto()
                {
                    BookId = x.Id,
                    BookTitle = x.Title,
                    Username = y.UserName ,
                    IsPublished = x.IsPublished,
                    Published = x.Published,
                    Verified = x.Verified,
                    Created = x.Created
                }
            );


            var result = new WebResult<IEnumerable<AdminBookDto>>()
            {
                Data = data,
                Success = true,
                PageNumber = filters.PageNumber,
                PageSize = filters.PageSize,
                Total = totalRecords,
            };

            return result;
        }

        public IOperationResult<AdminBookDto> ChangeVerification(AdminBookVerificationRequest request)
        {
            var book = _booksRepo.Data.FirstOrDefault(x => x.Id.Equals(request.BookId));
            
            if (book == null)
            {
                return new OperationResult<AdminBookDto>()
                {
                    Messages = new List<string>()
                    {
                        MessageStrings.NotFound
                    }
                };
            }

            book.Verified = request.Verified;

            _booksRepo.Update(book);

            var user = _usersRepo.Data.AsNoTracking()
                .FirstOrDefault(x => x.Id.Equals(book.OwnerId));

            var dto = _mapper.Map<AdminBookDto>(book);
            dto.Username = user.UserName;

            return new OperationResult<AdminBookDto>()
            {
                Data = dto,
                Success = true,
            };
        }
    }
}
