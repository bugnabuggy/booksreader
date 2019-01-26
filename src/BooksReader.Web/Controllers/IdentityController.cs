using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Infrastructure.Models;
using BooksReader.Infrastructure.Models.DTO;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using BooksReader.Web.Configuration;
using BooksReader.Infrastructure.Configuration;

namespace BooksReader.Web.Controllers
{
    [Produces("application/json")]
    [Route("api/Identity")]
    public class IdentityController : Controller
    {
		private UserManager<BrUser> _userManager;

		public IdentityController(UserManager<BrUser> userManager)
		{
			_userManager = userManager;
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

    }
}
