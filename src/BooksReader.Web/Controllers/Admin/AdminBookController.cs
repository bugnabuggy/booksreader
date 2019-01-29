using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Models;
using BooksReader.Core.Services;
using BooksReader.Infrastructure.Configuration;
using BooksReader.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BooksReader.Web.Controllers.Admin
{
    [Route("api/admin/books")]
    [Authorize(Roles = SiteRoles.Admin)]
    [ApiController]
    public class AdminBookController : ControllerBase
    {
        private readonly IBooksService _booksService;
        private readonly UserManager<BrUser> _userManager;

        public AdminBookController(
            IBooksService booksService,
            UserManager<BrUser> userManager)
        {
            _booksService = booksService;
            _userManager = userManager;
        }

        // GET: api/AdminBook
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

        // GET: api/AdminBook/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/AdminBook
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/AdminBook/5
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
