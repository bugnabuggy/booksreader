using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Services;
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
        public UserController(
            UserManager<BrUser> userManager,
            IUsersService usersService,
            ITranslationService translationService) : base(userManager, usersService, translationService)
        {
        }

        [HttpPut("profile")]
        public async Task<IActionResult> UpdateProfile([FromBody] UserProfileRequest profile)
        {
            //_usersService.UpdateUserProfile();
            return null;
        }

        
    }
}