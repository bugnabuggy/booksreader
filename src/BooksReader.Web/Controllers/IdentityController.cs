using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Models;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Services;
using BooksReader.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using BooksReader.Web.Configuration;
using BooksReader.Infrastructure.Configuration;
using BooksReader.Web.Models;
using Microsoft.EntityFrameworkCore.Query.Expressions;
using Newtonsoft.Json.Linq;

namespace BooksReader.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Identity")]
    public class IdentityController : Controller
    {
		private UserManager<BrUser> _userManager;
	    private readonly IUsersService _usersService;

		public IdentityController(UserManager<BrUser> userManager, IUsersService usersService)
		{
			_userManager = userManager;
			_usersService = usersService;
		}

		[HttpGet]
        public IActionResult Get()
        {
            return new JsonResult(from c in User.Claims select new { c.Type, c.Value });
        }
        
        [HttpGet("antiforgery")]

        public IActionResult GetAntiforgeryKey()
        {
            var key = Guid.NewGuid();
            var validTill = DateTime.UtcNow.AddMinutes(5);

            // cleanup
            var keysToRemove = StaticLists.AntiforgeryKeys.Where(pair => pair.Value <= DateTime.UtcNow);
            foreach (var keyValuePair in keysToRemove)
            {
                StaticLists.AntiforgeryKeys.Remove(keyValuePair.Key);
            }

            StaticLists.AntiforgeryKeys.Add(key, validTill);
            return Ok(key);
        }

	    [HttpPost("registration")]
	    public IActionResult Registration([FromBody]RegistrationRequest regModel)
	    {
		    var user = new BrUser() {UserName = regModel.Username };

			if (!_userManager.Users.Any(u => u.UserName.Equals(regModel.Username)))
		    {
			    var task = _userManager.CreateAsync(user, regModel.Password);
			    task.Wait(Constants.AsyncTaskWaitTime);
			    var result = task.Result;
			    if (!result.Succeeded)
			    {
				    return BadRequest(result.Errors);
			    }
		    }
		    return Ok();
	    }

		[HttpGet("me")]
		[Authorize]
	    public async Task<IActionResult> Me()
		{
			var user = await _userManager.GetUserAsync(User);

			return Ok(new {
				Username = user.UserName,
                Name = user.Name,
				user.Id,
				Roles = await _userManager.GetRolesAsync(user)
			});

		}

		[HttpGet("login-history")]
		[Authorize]
		public async Task<WebResult<IEnumerable<LoginHistoryResult>>> GetLogHistory(StandardFiltersDto filters)
	    {
		    var user = await _userManager.GetUserAsync(User);
		    var logHistory = this._usersService.GetLoginHistory(filters, user.Id, out int totalItems);
		    //logHistory.Sort((log1, log2) => log2.DateTime.CompareTo(log1.DateTime));

		    return new WebResult<IEnumerable<LoginHistoryResult>>()
		    {
				Data = logHistory,
				Total = totalItems,
			    PageNumber = filters.PageNumber ?? 0,
			    PageSize = filters.PageSize ?? 0,
			};

		    //logHistory;
	    }

	    [HttpPost("login-history")]
	    [Authorize]
		public async Task<IActionResult> AddLogHistory([FromBody]LoginHistoryRequest loginHistory)
	    {
			var user = await _userManager.GetUserAsync(User);

		    var logHistory = this._usersService.AddLoginHistory(new LoginHistory
		    {
			    DateTime = DateTime.Now,
			    IpAddress = HttpContext.Connection.RemoteIpAddress.ToString(),
			    Browser = HttpContext.Request.Headers["User-Agent"].ToString(),
				Screen = Newtonsoft.Json.JsonConvert.SerializeObject(loginHistory.Screen),
				Geolocation = Newtonsoft.Json.JsonConvert.SerializeObject(loginHistory.Coordinates)
			}, user.Id);

		    return Ok();
	    }


	}
}
