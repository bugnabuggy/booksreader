using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using Ruteco.AspNetCore.Translate;
using BooksReader.Web.Filters;

namespace BooksReader.Web.Controllers
{
    [UserActionFilter]
    public abstract class BaseUserController : BaseController
    {
        protected readonly UserManager<BrUser> _userManager;
        protected readonly IUsersService _usersService;
        protected BrUser user;

        protected BaseUserController(
            UserManager<BrUser> userManager,
            IUsersService usersService, 
            ITranslationService translationService
            ) : base(translationService)
        {
            _userManager = userManager;
            _usersService = usersService;
        }

        public async Task SetUser()
        {
            user = await _userManager.GetUserAsync(User);
        }
    }
}