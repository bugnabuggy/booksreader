using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Services;
using BooksReader.Infrastructure.Configuration;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Ruteco.AspNetCore.Translate;

namespace BooksReader.Web.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class PublicController : ControllerBase
    {
        private readonly IPublicService _publicSvc;

        public PublicController(
            IPublicService publicSvc
            )
        {
            _publicSvc = publicSvc;
        }

        [HttpGet]
        public ActionResult GetMainPage(
            [FromHeader] string domain,
            [FromHeader] string urlPath,
            [FromHeader] string promoCode)
        {
            var request = new PublicPageInfoRequest()
            {
                UrlPath = urlPath,
                Domain = domain,
                PromoCode = promoCode
            };
            var info = _publicSvc.GetInfo(request);
            return Ok(info);
        }
    }
}