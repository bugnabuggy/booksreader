using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.DTO.Admin;
using BooksReader.Core.Models.DTO.Public;
using BooksReader.Core.Models.Requests.Admin;
using BooksReader.Core.Models.Requests.Public;
using BooksReader.Core.Services;
using BooksReader.Infrastructure.Services;
using BooksReader.Validators.FilterAttributes;
using BooksReader.Validators.Getters;
using BooksReader.Validators.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BooksReader.Web.Controllers.Public
{
    [Route("api/book-market")]
    [ApiController]
    public class BookMarketController : BaseUserController
    {
        private readonly IBookMarketService _bookMarketSvc;

        public BookMarketController(
            UserManager<BrUser> userManager,
            
            IBookMarketService bookMarketSvc
            ) : base(userManager)
        {
            _bookMarketSvc = bookMarketSvc;
        }

        [HttpGet]
        public ActionResult<IWebResult<IEnumerable<BookMarketDto>>> Get([FromQuery] BookMarketFilters filters)
        {
            var result = _bookMarketSvc.GetBooks(filters, BrUser);

            return StandardReturn(result);
        }

        [HttpGet("{id}")]
        [Validate(typeof(Getter<Book>),
            new[]
            {
                typeof(ItemExistsValidator),
            })]
        public ActionResult<BookMarketDto> Get([FromRoute] Guid id)
        {
            var result = _bookMarketSvc.GetBook(id, BrUser);

            return Ok(result);
        }

        [HttpPost("{id}/add")]
        [Authorize]
        [Validate(typeof(Getter<Book>),
            new[]
            {
                typeof(ItemExistsValidator),
            })]
        public ActionResult<IOperationResult<BookSubscription>> Add([FromRoute] Guid id)
        {
            var result = _bookMarketSvc.Add(id, BrUser);
            return StandardReturn(result);
        }
    }
}
