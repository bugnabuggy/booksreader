using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Core;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.DTO.Admin;
using BooksReader.Core.Models.DTO.Public;
using BooksReader.Core.Models.DTO.Reader;
using BooksReader.Core.Models.Requests.Admin;
using BooksReader.Core.Models.Requests.Public;
using BooksReader.Core.Models.Requests.Reader;
using BooksReader.Core.Services;
using BooksReader.Core.Services.Reader;
using BooksReader.Dictionaries.Messages;
using BooksReader.Infrastructure.Services;
using BooksReader.Validators.FilterAttributes;
using BooksReader.Validators.Getters;
using BooksReader.Validators.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BooksReader.Web.Controllers.Reader
{
    [Route("api/reader/books")]
    [ApiController]
    public class ReaderBooksController : BaseUserController
    {
        private readonly IReaderDashboardService _readerDashboardSvc;

        public ReaderBooksController(
            UserManager<BrUser> userManager,
            
            IReaderDashboardService readerDashboardSvc
            ) : base(userManager)
        {
            _readerDashboardSvc = readerDashboardSvc;
        }

        [HttpGet]
        public async Task<ActionResult<IWebResult<IEnumerable<ReaderDashboardBookDto>>>> Get([FromQuery] ReaderDashboardFilters filters)
        {
            if (filters.UserId.HasValue)
            {
                var isAdmin = await _userManager.IsInRoleAsync(BrUser, SiteRoles.Admin);

                // user can ask for books list of other users only if he is in admin role
                if (!isAdmin
                    && !filters.UserId.Equals(BrUser.Id))
                {
                    return Forbid(MessageStrings.DoNotHavePermissions);
                }
            }
            else
            {
                filters.UserId = BrUser.Id;
            }

            var result = _readerDashboardSvc.GetReaderBooks(filters, BrUser);

            return StandardReturn(result);
        }

        [HttpDelete("{bookId}")]
        public ActionResult<IOperationResult<object>> RemoveSubscription([FromRoute] Guid bookId)
        {
            var result = _readerDashboardSvc.RemoveSubscription(bookId, BrUser);
            return StandardReturn(result);
        }

    }
}
