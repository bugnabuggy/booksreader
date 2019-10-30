using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models;
using BooksReader.Dictionaries.Messages;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BooksReader.Web.Filters;

namespace BooksReader.Web.Controllers
{
    [UserActionFilter]
    public abstract class BaseUserController : ControllerBase
    {
        protected readonly UserManager<BrUser> _userManager;
        protected BrUser BrUser;

        protected BaseUserController(
            UserManager<BrUser> userManager
            ) 
        {
            _userManager = userManager;
        }

        public async Task GetUser()
        {
            BrUser = await _userManager.GetUserAsync(User);
        }

        [NonAction]
        protected ActionResult<IOperationResult<T>> StandardReturn<T>(IOperationResult<T> result)
        {
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [NonAction]
        protected ActionResult<IWebResult<T>> StandardReturn<T>(IWebResult<T> result)
        {
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [NonAction]
        protected ActionResult StandardReturn(IOperationResult result)
        {
            if (result.Success)
            {
                return Ok(result);
            }
            else
            {
                return BadRequest(result);
            }
        }

        [NonAction]
        protected ActionResult<IOperationResult<T>> CheckWrongId<T>(IIdentified data, Guid id)
        {
            var wrongId = (data.Id != Guid.Empty) && (data.Id != id);
            if (wrongId)
            {
                return BadRequest(new OperationResult<Book>()
                {
                    Messages = new List<string>()
                    {
                        MessageStrings.RequestedIdNotEqualDataId
                    },
                });
            }

            return null;
        }

    }
}