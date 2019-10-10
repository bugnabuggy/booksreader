using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Services;
using BooksReader.Infrastructure.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BooksReader.Web.Controllers.Admin
{
    [Route("api/admin/users")]
    [ApiController]
    [Authorize(Roles = "Admin")]
    public class UsersController : ControllerBase
    {
        private readonly IUsersService _usersService;

        public UsersController(IUsersService usersService)
        {
            _usersService = usersService;
        }

        // GET: api/Users
        [HttpGet]
        public WebResult<IEnumerable<UserResult>> Get()
        {
            var data = _usersService.GetUsersWithRoles().ToList();

            var result = new WebResult<IEnumerable<UserResult>>
            {
                Data = data,
                Total = data.Count()
            };

            return result;
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public string Get(int id)
        {
            return "value";
        }

        [HttpPost("{username}/{role}")]
        public async Task<OperationResult> Post([FromRoute] string username, [FromRoute] string role)
        {
            var result = await _usersService.AddUserRole(username, role);

            return result;
        }

        [HttpDelete("{username}/{role}")]
        public async Task<OperationResult> Delete([FromRoute] string username, [FromRoute] string role)
        {
            var result = await _usersService.RemoveUserRole(username, role);

            return result;
        }

        [HttpPut("{username}/{role}")]
        public async Task<OperationResult> Put([FromRoute] string username, [FromRoute] string role)
        {
            var result = await _usersService.ToggleUserRole(username, role);

            return result;
        }

        // POST: api/Users
        [HttpPost]
        public void Post([FromBody] string user)
        {
        }

        // PUT: api/Users/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{username}")]
        public async Task<IActionResult> Delete(string username)
        {
            var result = await this._usersService.Delete(username);
            if (result.Success)
            {
                return  Ok(result);
            }

            return BadRequest(result.Messages);
        }
    }
}
