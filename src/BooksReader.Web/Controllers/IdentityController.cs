﻿using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Net;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Configuration;
using BooksReader.Core;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Services;
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
        private IUsersService _usersService;

		public IdentityController(
            UserManager<BrUser> userManager,
            IUsersService usersService)
		{
			_userManager = userManager;
            _usersService = usersService;
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
        //    var validTill = DateTimeOffset.UtcNow.AddMinutes(5);

        //    // cleanup, get lists 
        //    var keysToRemove = StaticLists.AntiforgeryKeys.Where(pair => pair.Value <= DateTimeOffset.UtcNow).ToList();
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

        [HttpGet("login-history")]
        [Authorize]
        public async Task<WebResult<IEnumerable<LoginHistory>>> GetLogHistory(StandardFilters filters)
        {
            var user = await _userManager.GetUserAsync(User);
            var logHistory = this._usersService.GetLoginHistory(filters, user.Id, out int totalItems);

            return new WebResult<IEnumerable<LoginHistory>>()
            {
                Data = logHistory,
                Total = totalItems,
                PageNumber = filters.PageNumber ?? 0,
                PageSize = filters.PageSize ?? 0,
            };
        }

        [HttpPost("login-history")]
        [Authorize]
        public async Task<IActionResult> AddLogHistory([FromBody]LoginHistoryRequest loginHistory)
        {
            var user = await _userManager.GetUserAsync(User);

            var logHistory = this._usersService.AddLoginHistory(new LoginHistory
            {
                DateTimeOffset = DateTimeOffset.Now,
                IpAddress = HttpContext.Connection.RemoteIpAddress.ToString(),
                Browser = HttpContext.Request.Headers["User-Agent"].ToString(),
                Screen = Newtonsoft.Json.JsonConvert.SerializeObject(loginHistory.Screen),
                Geolocation = Newtonsoft.Json.JsonConvert.SerializeObject(loginHistory.Coordinates)
            }, user.Id);

            return Ok();
        }

        [HttpPost("change-password")]
        [Authorize]
        public async Task<IActionResult> ChangePassword([FromBody]ChangePasswordRequest changePassword)
        {
            var user = await _userManager.GetUserAsync(User);
            var result = await _userManager.ChangePasswordAsync(user, changePassword.OldPassword, changePassword.NewPassword);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.Errors.Select(x => x.Code));
            }

        }

    }
}
