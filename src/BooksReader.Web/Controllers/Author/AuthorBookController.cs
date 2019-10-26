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
    [Route("api/author/book")]
    [Authorize(Roles = SiteRoles.Admin +", "+ SiteRoles.Author)]
    [ApiController]
    public class AuthorBookController : BaseUserController
    {
        private readonly IAuthorProfileService _authorProfileSvc;
        private readonly IBookEditingService _bookEditingSvc;

        public AuthorBookController (
            IAuthorProfileService authorProfileSvc,
            IBookEditingService bookEditingSvc,
            UserManager<BrUser> userManager): base(userManager)
        {
            _authorProfileSvc = authorProfileSvc;
            _bookEditingSvc = bookEditingSvc;
        }

        [HttpGet]
        public async Task<ActionResult<IWebResult<IEnumerable<Book>>>> Get([FromQuery] StandardBooksFilters filters)
        {
            var isAdmin = await _userManager.IsInRoleAsync(BrUser, SiteRoles.Admin);

            // user can ask for books list of other users only if he is in admin role
            if (!isAdmin
                && filters.UserId.HasValue
                && !filters.UserId.Equals(BrUser.Id))
            {
                return Forbid(MessageStrings.DoNotHavePermissions);
            }

            var result = _bookEditingSvc.GetBooks(filters);

            return StandardReturn(result);
        }

        [HttpPost]
        public ActionResult<IOperationResult<Book>> Add([FromBody] BookBasicInfoRequest data)
        {
            var result = _bookEditingSvc.Add(data, BrUser);

            return StandardReturn(result);
        }

        [HttpPut("{id}")]
        [Validate(typeof(Getter<Book>),
            new[]
            {
                typeof(ItemExistsValidator),
                typeof(OwnerOrAdministratorValidator)
            })]
        public ActionResult<IOperationResult<Book>> Edit([FromRoute] Guid id, [FromBody] BookBasicInfoRequest data)
        {
            var wrongId = (data.Id != Guid.Empty) && (data.Id != id);
            if (wrongId)
            {
                return BadRequest(new OperationResult<Book>()
                {
                    Messages = new List<string>()
                    {
                        MessageStrings.BooksMessages.RequestedIdNotEqualBookData
                    },
                });
            }
            
            var result = _bookEditingSvc.Edit(data, BrUser);

            return StandardReturn(result);
        }

        [HttpDelete("{id}")]
        [Validate(typeof(Getter<Book>),
            new[]
            {
                typeof(ItemExistsValidator),
                typeof(OwnerOrAdministratorValidator)
            })]
        public ActionResult<IOperationResult<Book>> Delete(Guid id)
        {
            var result = _bookEditingSvc.Delete(id, BrUser);

            return StandardReturn(result);
        }

        [HttpGet("{id}/edit")]
        [Validate(typeof(Getter<Book>),
            new[]
            {
                typeof(ItemExistsValidator),
                typeof(OwnerOrAdministratorValidator)
            })]
        public ActionResult<IOperationResult<BookFullEditInfoDto>> GetFull([FromRoute] Guid id)
        {
            var result = _bookEditingSvc.Get(id);

            return StandardReturn(result);
        }


        [HttpPut("{id}/edit")]
        [Validate(typeof(Getter<Book>),
            new[]
            {
                typeof(ItemExistsValidator),
                typeof(OwnerOrAdministratorValidator)
            })]
        public ActionResult<IOperationResult<Book>> EditFull([FromRoute] Guid id, [FromBody] BookEditRequest book)
        {
            var idCheck = CheckWrongId<Book>(book, id);
            if (idCheck != null) return idCheck;


            var result = _bookEditingSvc.EditFull(book, BrUser);

            return StandardReturn(result);
        }
    }
}