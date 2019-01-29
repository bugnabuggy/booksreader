using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Exceptions;
using BooksReader.Core.Models;
using BooksReader.Core.Services;
using BooksReader.Infrastructure.Configuration;
using BooksReader.Infrastructure.Models;
using BooksReader.Web.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BooksReader.Web.Controllers.Author
{
    [Route("api/book")]
	[Authorize]
    [ApiController]
    public class BookController : ControllerBase
    {
	    private readonly IBooksService _booksService;
	    private readonly UserManager<BrUser> _userManager;

		public BookController(
			IBooksService booksService, 
			UserManager<BrUser> userManager)
	    {
		    _booksService = booksService;
		    _userManager = userManager;
	    }

		// GET: api/Book
        [HttpGet]
        public async Task<WebResult<IEnumerable<Book>>> Get()
        {
		    var user = await _userManager.GetUserAsync(User);
	        var isAdmin = await _userManager.IsInRoleAsync(user, SiteRoles.Admin);

			var books = isAdmin
						? _booksService.Get().ToList()
						: _booksService.Get(user.Id).ToList();

	        return new WebResult<IEnumerable<Book>>
			{ 
				Data = books,
				Success = true,
				Total = books.Count
	        };
        }

        // GET: api/Book/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Book
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
					Created = DateTime.Now,
					OwnerId = Guid.Parse(user.Id)
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
			        Messages = new List<string>() {exp.Message}
		        };
	        }
        }

        // PUT: api/Book/5
        [HttpPut("{id}")]
        public OperationResult Put([FromBody] BookEditRequest model)
        {
	        try
	        {
		        var book = _booksService.Edit(new Book()
		        {
			        Id = model.Id,
			        Title = model.Title,
			        Author = model.Author
		        });
		        return new OperationResult<Book>()
		        {
			        Data = book,
			        Success = true,
		        };
			}
	        catch (Exception exp)
	        {
				return new OperationResult<BookEditRequest>()
				{
					Data = model,
					Success = false,
					Messages = new List<string>() { exp.Message }
				};
			}
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public OperationResult Delete(string bookId)
        {
	        try
	        {
		        var book = _booksService.Delete(bookId);
		        return new OperationResult<Book>()
		        {
			        Data = book,
			        Success = true,
		        };
	        }
	        catch (Exception exp)
	        {
		        return new OperationResult<BookEditRequest>()
		        {
			        Data = {},
			        Success = false,
			        Messages = new List<string>() { exp.Message }
		        };
	        }
		}
    }
}
