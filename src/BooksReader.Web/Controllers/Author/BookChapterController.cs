﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Models;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Services;
using BooksReader.Validators;
using BooksReader.Validators.FilterAttributes;
using BooksReader.Validators.Getters;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Ruteco.AspNetCore.Translate;

namespace BooksReader.Web.Controllers.Author
{
    [Route("api/author/book/{bookId}/chapter")]
    [ApiController]
    public class BookChapterController : BaseUserController
    {
        private readonly IBookChapterService _bookChapterService;

        public BookChapterController(
            IBookChapterService bookChapterService,
            UserManager<BrUser> userManager
            ) : base(userManager)
        {
            _bookChapterService = bookChapterService;
        }
        
        [HttpGet]
        public IEnumerable<BookChapter> GetByBook([FromRoute]Guid bookId)
        {
            return _bookChapterService.GetByBook(bookId);
        }

        [HttpGet("{id}")]
        public BookChapter Get(Guid id)
        {
            return _bookChapterService.Get(id);
        }

        /// <summary>
        /// Creation or quick update of book chapter
        /// </summary>
        /// <param name="bookId"></param>
        /// <param name="item">request with basic chapter data</param>
        /// <returns></returns>
        [HttpPost]
        public IOperationResult<BookChapter> Post([FromRoute] Guid bookId, [FromBody] BookChapterRequest item)
        {
            var result = _bookChapterService.AddOrUpdate(bookId, item);
            return result;
        }

        [HttpPost("reorder")]
        public IOperationResult<IEnumerable<BookChapterReorderRequest>> Post([FromRoute] Guid bookId, [FromBody] IEnumerable<BookChapterReorderRequest> order)
        {
            var result = _bookChapterService.Reorder(bookId, order);
            return result;
        }

        [HttpPut("{id}")]
        [Validate(
            typeof (Getter<BookChapter>),
            new Type[]
            {
                typeof(ItemExistsValidator),
                typeof(OwnerOrAdministratorValidator)
            },
            "id"
            )]
        public IActionResult Put([FromRoute] Guid bookId, [FromRoute] Guid id, [FromBody] BookChapter chapter)
        {
            var result = this._bookChapterService.EditContent(bookId, chapter);
            if (result.Success) 
                return Ok(result);
            else
            {
                return BadRequest(result);
            }

        }

        [HttpDelete("{id}")]
        public IOperationResult<BookChapter> Delete(Guid id)
        {
            var result = _bookChapterService.Delete(id);
            return result;
        }
    }
}
