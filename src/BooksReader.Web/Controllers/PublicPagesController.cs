using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Configuration;
using BooksReader.Core;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Services;
using BooksReader.Dictionaries.Messages;
using BooksReader.Validators.FilterAttributes;
using BooksReader.Validators.Getters;
using BooksReader.Validators.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ruteco.AspNetCore.Translate;

namespace BooksReader.Web.Controllers
{
    [Route("api/public-pages")]
    [Authorize]
    public class PublicPagesController : BaseUserController
    {
        private readonly IPublicPagesService _publicPagesSvc;

        public PublicPagesController (
            UserManager<BrUser> userManager,
            IPublicPagesService publicPagesSvc
            ) : base(userManager)
        {
            _publicPagesSvc = publicPagesSvc;
        }


        [HttpGet]
        public IActionResult Get()
        {
            return Ok();
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok();
        }

        [HttpPost]
        public ActionResult<IOperationResult<PublicPage>> Post([FromBody]PublicPage page)
        {
            var result = _publicPagesSvc.Add(page, BrUser);
            return StandardReturn(result);
        }

        [HttpPut("{id}")]
        [Validate(typeof(Getter<PublicPage>),
            new[]
            {
                typeof(ItemExistsValidator),
            })]
        public ActionResult<IOperationResult<PublicPage>> Put(Guid id, [FromBody] PublicPage page)
        {
            var result = _publicPagesSvc.Update(page, BrUser);
            return StandardReturn(result);
        }

        [HttpDelete("{id}")]
        [Validate(typeof(Getter<PublicPage>),
            new[]
            {
                typeof(ItemExistsValidator),
            })]
        public ActionResult<IOperationResult<PublicPage>> Delete(Guid id)
        {
            var result = _publicPagesSvc.Delete(id, BrUser);
            return StandardReturn(result);
        }
    }
}