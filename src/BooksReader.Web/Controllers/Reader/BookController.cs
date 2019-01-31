using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.PeerToPeer.Collaboration;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Models;
using BooksReader.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksReader.Web.Controllers.Reader
{
    [Route("api/books")]
    [ApiController]
    public class BookController : ControllerBase
    {
	    private readonly IBooksService _booksService;

	    public BookController(IBooksService booksService)
	    {
		    _booksService = booksService;
	    }
		// GET: api/Book
	    [HttpGet]
	    public WebResult<IEnumerable<Book>> Get()
	    {
		    var books = _booksService.Get().ToList();

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
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Book/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
