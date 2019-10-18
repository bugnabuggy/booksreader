using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Configuration;
using BooksReader.Core;
using BooksReader.Core.Entities;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.DTO.Author;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Services;
using BooksReader.Dictionaries;
using BooksReader.Validators.FilterAttributes;
using BooksReader.Validators.Getters;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using MessageStrings = BooksReader.Dictionaries.Messages.MessageStrings;

namespace BooksReader.Web.Controllers.Author
{
    [Route("api/author/book")]
    [Authorize]
    [ApiController]
    public class AuthorBookController : BaseUserController
    {
        private readonly IAuthorProfileService _authorProfileSvc;

        public AuthorBookController (
            IAuthorProfileService authorProfileSvc,
            UserManager<BrUser> userManager): base(userManager)
        {
            _authorProfileSvc = authorProfileSvc;
            
        }

      
    }
}