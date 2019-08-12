using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Exceptions;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Services;
using BooksReader.Core.Services.Author;
using BooksReader.Dictionaries;
using BooksReader.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ruteco.AspNetCore.Translate;

namespace BooksReader.Web.Controllers.Author
{
    [Route("api/author/profile")]
    [Authorize]
    [ApiController]
    public class AuthorProfileController : BaseUserController
    {
        private readonly IAuthorProfileService _authorProfileSvc;

        public AuthorProfileController(
            IAuthorProfileService authorProfileSvc,
            IUsersService usersService,
            ITranslationService translations,
            UserManager<BrUser> userManager): base(userManager, usersService, translations)
        {
            _authorProfileSvc = authorProfileSvc;
        }

        [HttpGet]
        public ActionResult Get()
        {
            var result = _authorProfileSvc.GetAuthorProfile(user);
            if (result != null)
            {
                return Ok(new AuthorProfileDto()
                {
                    Id = result.Id,
                    AuthorName = result.AuthorName,
                    Description = result.Description,
                    DomainName = result.PersonalPage?.Domain,
                    UrlPath = result.PersonalPage?.UrlPath,
                    PageContent = result.PersonalPage?.Content
                });
            }

            var lang = string.IsNullOrEmpty(user.Language) ? Constants.DefaultLanguage : user.Language;
            return NotFound(_translationService.Get(lang, MessageStrings.AuthorNotFound));
        }

        [HttpPut("{username}")]
        public async Task<ActionResult> UpdateAuthorProfile(string username, [FromBody] AuthorProfileDto dto)
        {
            if (!user.UserName.Equals(username) || await _userManager.IsInRoleAsync(user, SiteRoles.Admin))
            {
                return Forbid(MessageStrings.DoNotHavePermissions);
            }

            var result = _authorProfileSvc.EditAuthorProfile(dto);

            if (!result.Success)
            {
                return BadRequest(result); 
            }

            return Ok(result);
        }
    }
}