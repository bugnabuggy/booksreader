using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ruteco.AspNetCore.Translate;

namespace BooksReader.Web.Controllers
{
    public abstract class BaseUserController : BaseController
    {
        protected readonly UserManager<BrUser> _userManager;
        protected readonly IUsersService _usersService;

        protected BaseUserController(
            UserManager<BrUser> userManager,
            IUsersService usersService, 
            ITranslationService translationService
            ) : base(translationService)
        {
            _userManager = userManager;
            _usersService = usersService;
        }
    }
}