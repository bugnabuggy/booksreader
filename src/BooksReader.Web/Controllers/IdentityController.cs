using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
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

		[HttpGet("getLogHistory")]
		[Authorize]
		public async Task<List<LoginHistoryResult>> GetLogHistory()
	    {
		    var user = await _userManager.GetUserAsync(User);
		    var logHistory = await this._usersService.GetLogHistory(user.Id);
		    logHistory.Sort(delegate (LoginHistoryResult log1, LoginHistoryResult log2)
		    {
			    return log2.DateTime.CompareTo(log1.DateTime);
		    });

		    return logHistory;
	    }

	    [HttpPost("addLogHistory")]
	    [Authorize]
		public async Task<IActionResult> AddLogHistory([FromBody]GeolocationRequest geolocationRequest)
	    {
		    var geolocation = "not found geolocation";

			var user = await _userManager.GetUserAsync(User);
		    if (geolocationRequest.Latitude >= 0 && geolocationRequest.Latitude >= 0)
		    {
			    geolocation = "Latitude: " + geolocationRequest.Latitude + ", Longitude: " + geolocationRequest.Longitude;

		    }
		    var logHistory = this._usersService.AddLogHistory(new LoginHistoryResult
		    {
			    DateTime = DateTime.Now,
			    IpAddress = HttpContext.Connection.RemoteIpAddress.ToString(),
			    Browser = HttpContext.Request.Headers["User-Agent"].ToString(),
			    Geolocation = geolocation
			}, user.Id);

		    return Ok();
	    }


	}
}
