using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Services;
using BooksReader.Dictionaries;
using BooksReader.Infrastructure.Configuration;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Ruteco.AspNetCore.Translate;

namespace BooksReader.Web.Controllers.User
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class UserController : BaseUserController
    {
        private readonly ISecurityService _security;

        public UserController(
            UserManager<BrUser> userManager,
            IUsersService usersService,
            ISecurityService security,
            ITranslationService translationService) : base(userManager, usersService, translationService)
        {
            _security = security;
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserProfileRequest profile)
        {
            var user = await _userManager.GetUserAsync(User);

            if (await _userManager.IsInRoleAsync(user, SiteRoles.Admin) || user.UserName.Equals(profile.Username))
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

        
    }
}