using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.DTO.Admin;
using BooksReader.Core.Models.Requests.Admin;
using BooksReader.Core.Services;
using BooksReader.Infrastructure.Services;
using BooksReader.Validators.FilterAttributes;
using BooksReader.Validators.Getters;
using BooksReader.Validators.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BooksReader.Web.Controllers.Admin
{
    [Route("api/admin/books")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class BooksMarketController : BaseUserController
    {
        private readonly IUsersService _usersService;
        private readonly IAdminBooksService _adminBooksSvc;

        public BooksMarketController(
            UserManager<BrUser> userManager,
            IUsersService usersService,
            IAdminBooksService adminBooksSvc
            ) : base(userManager)
        {
            _usersService = usersService;
            _adminBooksSvc = adminBooksSvc;
        }

        [HttpGet]
        public ActionResult<IWebResult<IEnumerable<AdminBookDto>>> Get([FromQuery] AdminAllBooksFilter filters)
        {
            var result = _adminBooksSvc.GetBooks(filters);

            return StandardReturn(result);
        }

        [HttpPost("verification")]
        public ActionResult<IOperationResult<AdminBookDto>> Post([FromBody]AdminBookVerificationRequest  request)
        {
            var result = _adminBooksSvc.ChangeVerification(request);

            return StandardReturn(result);
        }
    }
}
