using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Configuration;
using BooksReader.Core;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.DTO.Author;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Models.Requests.Author;
using BooksReader.Core.Services;
using BooksReader.Dictionaries;
using BooksReader.Validators.FilterAttributes;
using BooksReader.Validators.Getters;
using BooksReader.Validators.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MessageStrings = BooksReader.Dictionaries.Messages.MessageStrings;

namespace BooksReader.Web.Controllers.Author
{
    [Route("api/author/book-price")]
    [Authorize(Roles = SiteRoles.Admin + ", " + SiteRoles.Author)]
    [ApiController]
    public class AuthorBookPriceController : BaseUserController
    {
        private readonly IBookPricesService _bookPricesSvc;

        public AuthorBookPriceController(
            IBookPricesService bookPricesSvc,
            UserManager<BrUser> userManager): base(userManager)
        {
            _bookPricesSvc = bookPricesSvc;
        }

        [HttpGet("{bookId}")]
        [Validate(typeof(Getter<Book>),
            new[]
            {
                typeof(ItemExistsValidator),
                typeof(OwnerOrAdministratorValidator)
            },
            "bookId")
        ]
        public ActionResult<IOperationResult<IEnumerable<BookPrice>>> GetPrices([FromRoute] Guid bookId)
        {
            var result = _bookPricesSvc.GetPrices(bookId);

            return StandardReturn(result);
        }

        [HttpPost]
        public ActionResult<IOperationResult<BookPrice>> Add( [FromBody] BookPriceRequest priceData)
        {
            var result = _bookPricesSvc.Add(priceData, BrUser);

            return StandardReturn(result);
        }

        [HttpPut("{id}")]
        public ActionResult<IOperationResult<BookPrice>> Edit([FromRoute]Guid id, [FromBody] BookPriceRequest priceData)
        {
            var wrongIdResp = CheckWrongId<BookPrice>(priceData, id);
            if (wrongIdResp != null)
            {
                return wrongIdResp;
            }

            var result = _bookPricesSvc.Edit(priceData, BrUser);

            return StandardReturn(result);
        }

        [HttpDelete("{id}")]
        [Validate(typeof(Getter<BookPrice>),
            new[]
            {
                typeof(ItemExistsValidator),
                typeof(OwnerOrAdministratorValidator)
            })
        ]
        public ActionResult<IOperationResult<BookPrice>> Delete([FromRoute]Guid id)
        {
            var result = _bookPricesSvc.Delete(id, BrUser);

            return StandardReturn(result);
        }
    }
}