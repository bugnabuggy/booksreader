using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Enums;
using BooksReader.Core.Exceptions;
using BooksReader.Core.Infrastructure;
using BooksReader.Core.Models;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Models.Requests.Filters;
using BooksReader.Core.Services;
using BooksReader.Dictionaries;
using BooksReader.Infrastructure.Configuration;
using BooksReader.Infrastructure.Repositories;
using BooksReader.Validators;
using BooksReader.Validators.FilterAttributes;
using BooksReader.Validators.Getters;
using BooksReader.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MessageStrings = BooksReader.Dictionaries.Messages.MessageStrings;

namespace BooksReader.Web.Controllers.Author
{
    [Route("api/author/book")]
    [Authorize]
    [ApiController]
    public class AuthorBookController : BaseUserController
    {
        private readonly IBooksService _booksService;
        private readonly ISecurityService _security;

        public AuthorBookController(
            IBooksService booksService,

            ISecurityService security,
            UserManager<BrUser> userManager): base(userManager)
        {
            _booksService = booksService;
            _security = security;
        }


        // GET: api/Book
        [HttpGet]
        public async Task<WebResult<IEnumerable<Book>>> Get([FromQuery]AuthorBookFiltersRequest filters)
        {
            var user = await _userManager.GetUserAsync(User);

            var books = _booksService.GetByOwnerId(user.Id).ToList();

            return new WebResult<IEnumerable<Book>>
            {
                Data = books,
                Success = true,
                Total = books.Count
            };
        }

        [HttpGet("{id:guid}")]
        public OperationResult<Book> Get(Guid id)
        {
            var book = _booksService.Get(id);
            return new OperationResult<Book>()
            {
                Data = book
            };
        }

        [HttpPost]
        public async Task<OperationResult> Post([FromBody] BookEditRequest model)
        {
            var user = await _userManager.GetUserAsync(User);

            try
            {
                var book = _booksService.Add(new Book()
                {
                    Id = Guid.NewGuid(),
                    Title = model.Title,
                    Author = model.Author,
                    Created = DateTime.UtcNow,
                    OwnerId = user.Id
                });

                return new OperationResult<Book>()
                {
                    Data = book,
                    Success = true,
                };
            }
            catch (BrBadDataException exp)
            {
                Response.StatusCode = StatusCodes.Status400BadRequest;
                return new OperationResult<BookEditRequest>()
                {
                    Data = model,
                    Success = false,
                    Messages = new List<string>() { exp.Message }
                };
            }
        }

        [HttpPut("{id}")]
        public async Task<IActionResult> Put([FromBody] BookEditRequest model)
        {
            try
            {
                var book = _booksService.Get(model.Id);
                var user = await _userManager.GetUserAsync(User);

                if (!_security.HasAccess(user.Id, book))
                {
                    return Forbid(MessageStrings.DoNotHavePermissions);
                }

                // quick update
                book.Author = model.Author;
                book.Title = model.Title;

                book = _booksService.Edit(book);
                return Ok(new OperationResult<Book>()
                {
                    Data = book,
                    Success = true,
                });
            }
            catch (Exception exp)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new OperationResult<BookEditRequest>()
                {
                    Data = model,
                    Success = false,
                    Messages = new List<string>() { exp.Message }
                });
            }
        }

        [HttpDelete("{bookId}")]
        [Validate(typeof(Getter<Book>),
            new Type[]
                    {
                        typeof(ItemExistsValidator),
                        typeof(OwnerOrAdministratorValidator)
                    },
            "bookId")]
        public IActionResult Delete(Guid bookId)
        {
            try
            {
                var book = _booksService.Delete(bookId);
                return Ok(new OperationResult<Book>()
                {
                    Data = book,
                    Success = true,
                });
            }
            catch (Exception exp)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new OperationResult()
                {
                    Success = false,
                    Messages = new List<string>() { exp.Message }
                });
            }
        }

        [HttpGet("{bookId:guid}/edit")]
        [Validate(typeof(Getter<Book>),
            new Type[]
            {
                typeof(ItemExistsValidator),
                typeof(OwnerOrAdministratorValidator)
            },
            "bookId")]
        public IActionResult GetBookEditDto(Guid bookId)
        {
            var result = _booksService.GetFull(bookId);
            return StandartReturn(result);

        }

        [HttpPut("{bookId:guid}/edit")]
        [Validate(typeof(Getter<Book>),
            new Type[]
            {
                typeof(ItemExistsValidator),
                typeof(OwnerOrAdministratorValidator)
            },
            "bookId")]
        public IActionResult PutBookEditInfo(Guid bookId, [FromBody] BookEditFullRequest bookInfo)
        {
            bookInfo.Book.Id = bookId;
            var result = _booksService.SaveFull(bookInfo);
            return StandartReturn<BookEditFullRequest>(result);
        }

        
    }
}
