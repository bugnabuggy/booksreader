using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
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
                return Ok(result);
            }

            var lang = string.IsNullOrEmpty(user.Language) ? Constants.DefaultLanguage : user.Language;
            return NotFound(_translationService.Get(lang, MessageStrings.AuthorNotFound));
        }

        [HttpPut("{username}")]
        public Task<ActionResult> UpdateAuthorProfile(string username)
        {


            return null;
        }
    }
}