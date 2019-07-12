using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ruteco.AspNetCore.Translate;

namespace BooksReader.Web.Controllers
{
    [ApiController]
    public class PublicController : BaseController
    {
        private readonly IPublicService _publicSvc;

        public PublicController(
            ITranslationService translations,
            IPublicService publicSvc
            ) : base(translations)
        {
            _publicSvc = publicSvc;
        }

        [HttpGet]
        public ActionResult GetMainPage([FromHeader]PublicPageInfoRequest request)
        {
            var info = _publicSvc.GetInfo(request);
            return Ok(info);
        }
    }
}