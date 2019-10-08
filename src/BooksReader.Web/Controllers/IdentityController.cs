using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Configuration;
using BooksReader.Core.Entities;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.Requests;
using BooksReader.Dictionaries.Messages;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

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
        
        //[HttpGet("antiforgery")]
        //public IActionResult GetAntiforgeryKey()
        //{
        //    var key = Guid.NewGuid();
        //    var validTill = DateTime.UtcNow.AddMinutes(5);

        //    // cleanup, get lists 
        //    var keysToRemove = StaticLists.AntiforgeryKeys.Where(pair => pair.Value <= DateTime.UtcNow).ToList();
        //    foreach (var keyValuePair in keysToRemove)
        //    {
        //        StaticLists.AntiforgeryKeys.Remove(keyValuePair.Key);
        //    }

        //    StaticLists.AntiforgeryKeys.Add(key, validTill);
        //    return Ok(key);
        //}

	    [HttpPost("registration")]
	    public async Task<IActionResult> Registration([FromBody]RegistrationRequest regModel)
	    {
            //if (!StaticLists.AntiforgeryKeys.ContainsKey(Guid.Parse(regModel.AntiforgeryKey)))
            //{
            //    return BadRequest(MessageStrings.WrongAntiForgeryKey);
            //}

            var user = new BrUser() {UserName = regModel.Username, Name =  regModel.Fullname};

			if (!_userManager.Users.Any(u => u.UserName.Equals(regModel.Username)))
		    {
                var result = await _userManager.CreateAsync(user, regModel.Password);
			    if (!result.Succeeded)
			    {
				    return BadRequest(result.Errors);
			    }

                var roles = new[]
                {
                    SiteRoles.Reader,
                    SiteRoles.User
                };

                result = await _userManager.AddToRolesAsync(user, roles);
                if (!result.Succeeded)
                {
                    return BadRequest(result.Errors);
                }
                return Ok();
            }

            return BadRequest(MessageStrings.UserAlreadyExists);

        }

		[HttpGet("me")]
		[Authorize]
	    public async Task<IActionResult> Me()
		{
			var user = await _userManager.GetUserAsync(User);

            if (user == null)
            {
                return NotFound(MessageStrings.UserDoesNotExistOrDeleted);
            }

			return Ok(new AppUserDto(){
				Username = user.UserName,
                Name = user.Name,
                Avatar = user.Avatar,
                Email = user.Email,
				Id = user.Id.ToString(),
				Roles = await _userManager.GetRolesAsync(user)
			});

		}

    }
}
