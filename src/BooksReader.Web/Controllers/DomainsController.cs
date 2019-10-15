using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Configuration;
using BooksReader.Core;
using BooksReader.Core.Entities;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Models.Requests.Admin;
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
    [Route("api/[controller]")]
    [Authorize]
    public class DomainsController : BaseUserController
    {
        private readonly IDomainsService _domainssSvc;

        public DomainsController (
            UserManager<BrUser> userManager,
            IDomainsService domainsSvc
            ) : base(userManager)
        {
            _domainssSvc = domainsSvc;
        }


        [HttpGet]
        [Authorize(Roles =  SiteRoles.Admin)]
        public IActionResult Get(AllDomainsFilters filters)
        {
            var result = this._domainssSvc.Get(filters);

            return StandartReturn(result);
        }

        [HttpGet("{id}")]
        public IActionResult Get(Guid id)
        {
            return Ok();
        }

        [HttpPost]
        public IActionResult Post([FromBody]UserDomainRequest domain)
        {
            var result = _domainssSvc.Add(domain, BrUser);
            return StandartReturn(result);
        }

        [HttpPut("{id}")]
        public IActionResult Put(Guid id, [FromBody] UserDomainRequest domain)
        {
            var result = _domainssSvc.Update(domain, BrUser);
            return StandartReturn(result);
        }

        [HttpDelete("{id}")]
        [Validate(typeof(Getter<UserDomain>),
            new[]
            {
                typeof(ItemExistsValidator),
                typeof(OwnerOrAdministratorValidator)
            })]
        public IActionResult Delete(Guid id)
        {
            var result = _domainssSvc.Delete(id, BrUser);
            return StandartReturn(result);
        }
    }
}