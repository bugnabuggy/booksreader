using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
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

        public PublicController(
            IListsService listsSvc
            )
        {
            _listsSvc = listsSvc;
        }


        [HttpGet("lists")]
        public ActionResult GetLists()
        {
            var lists = _listsSvc.GetLists().ToList();
            return Ok(lists);
        }
    }
}