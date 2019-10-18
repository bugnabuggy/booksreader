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
    [Route("api/[controller]")]
    [ApiController]
    public class PublicController : ControllerBase
    {
        private readonly IListsService _listsSvc;
        private readonly IPublicService _publicSvc;

        public PublicController(
            IListsService listsSvc,
            IPublicService publicSvc
            )
        {
            _listsSvc = listsSvc;
            _publicSvc = publicSvc;
        }


        [HttpGet("lists")]
        public ActionResult GetLists()
        {
            var lists = _listsSvc.GetLists().ToList();
            return Ok(lists);
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