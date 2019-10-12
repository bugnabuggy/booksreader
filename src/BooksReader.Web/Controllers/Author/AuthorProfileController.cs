using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Configuration;
using BooksReader.Core;
using BooksReader.Core.Entities;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.DTO.Author;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Services;
using BooksReader.Dictionaries;
using BooksReader.Validators.FilterAttributes;
using BooksReader.Validators.Getters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MessageStrings = BooksReader.Dictionaries.Messages.MessageStrings;

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
            UserManager<BrUser> userManager): base(userManager)
        {
            _authorProfileSvc = authorProfileSvc;
            
        }

        [HttpGet("full")]
        public ActionResult GetFull()
        {
            var result = _authorProfileSvc.GetAuthorFullProfile(BrUser.Id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound(MessageStrings.AuthorNotFound);
        }

        [HttpGet]
        public ActionResult Get()
        {
            var result = _authorProfileSvc.GetAuthorProfile(BrUser.Id);
            if (result != null)
            {
                return Ok(result);
            }

            return NotFound(MessageStrings.AuthorNotFound);
        }

        [HttpPut("{username}")]
        public async Task<ActionResult> UpdateAuthorProfile(string username, [FromBody] AuthorProfileDto dto)
        {
            if (!BrUser.UserName.Equals(username) || await _userManager.IsInRoleAsync(BrUser, SiteRoles.Admin))
            {
                return Forbid(MessageStrings.DoNotHavePermissions);
            }

            var result = await _authorProfileSvc.EditAuthorProfile(dto);

            if (!result.Success)
            {
                return BadRequest(result); 
            }

            return Ok(result);
        }
    }
}