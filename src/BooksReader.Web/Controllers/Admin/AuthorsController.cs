using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Services;
using BooksReader.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BooksReader.Web.Controllers.Admin
{
    [Route("api/admin/authors")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class AuthorsController : BaseUserController
    {
        private readonly IUsersService _usersService;

        public AuthorsController(
            UserManager<BrUser> userManager,
            IUsersService usersService
            ) : base(userManager)
        {
            _usersService = usersService;
        }

        
    }
}
