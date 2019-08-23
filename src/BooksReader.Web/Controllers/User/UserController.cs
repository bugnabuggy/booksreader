using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Services;
using BooksReader.Core.Services.Author;
using BooksReader.Dictionaries;
using BooksReader.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Ruteco.AspNetCore.Translate;
using MessageStrings = BooksReader.Dictionaries.Messages.MessageStrings;

namespace BooksReader.Web.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseUserController
    {
        private readonly IAuthorProfileService  _authorProfileSvc;
        private readonly IUsersService _usersService;

        public UserController(
            UserManager<BrUser> userManager,
            IAuthorProfileService authorProfileSvc,
            IUsersService usersService,
            ITranslationService translationService) : base(userManager)
        {
            _authorProfileSvc = authorProfileSvc;
            _usersService = usersService;
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserProfileRequest profile)
        {
            if (await _userManager.IsInRoleAsync(BrUser, SiteRoles.Admin) || BrUser.UserName.Equals(profile.Username))
            {
                var result = await _usersService.Update(profile);
                if (result.Success)
                {
                    return Ok(result.Data);
                }

                return BadRequest(result.Messages);
            }

            return StatusCode((int)HttpStatusCode.Forbidden, MessageStrings.DoNotHavePermissions);
        }

        [HttpPost("author")]
        public async Task<ActionResult> Author([FromBody]AuthorRequest data)
        {

            var roleResult = await this._usersService.AddUserRole(BrUser.UserName, SiteRoles.Author);
            try
            {

            }
            catch (Exception e)
            {
                Console.WriteLine(e);
                throw;
            }
            var authorProfileResult = await this._authorProfileSvc.CreateAuthorProfile(BrUser);

            if (roleResult.Success && authorProfileResult.Success)
            {
                return Ok(authorProfileResult.Data);
            }

            return BadRequest();
        }
    }
}