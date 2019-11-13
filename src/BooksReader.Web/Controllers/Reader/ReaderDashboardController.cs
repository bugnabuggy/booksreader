using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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
    [Route("api/reader/dashboard")]
    [ApiController]
    public class ReaderDashboardController : BaseUserController
    {
        private readonly IReaderDashboardService _readerDashboardSvc;

        public ReaderDashboardController(
            UserManager<BrUser> userManager,
            
            IReaderDashboardService readerDashboardSvc
            ) : base(userManager)
        {
            _readerDashboardSvc = readerDashboardSvc;
        }

        [HttpGet]
        public ActionResult<IWebResult<IEnumerable<ReaderDashboardBookDto>>> Get([FromQuery] ReaderDashboardFilters filters)
        {
            var result = _readerDashboardSvc.GetReaderBooks(filters, BrUser);

            return StandardReturn(result);
        }
        
    }
}
