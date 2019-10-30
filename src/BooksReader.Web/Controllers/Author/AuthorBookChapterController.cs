using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Core;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.Requests.Author;
using BooksReader.Core.Services;
using BooksReader.Validators.FilterAttributes;
using BooksReader.Validators.Getters;
using BooksReader.Validators.Validators;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace BooksReader.Web.Controllers.Author
{
    [Route("api/author/book/{bookId}/chapter")]
    [ApiController]
    [Authorize(Roles = SiteRoles.Author + "," + SiteRoles.Admin)]
    [Validate(typeof(Getter<Book>),
        new[]
        {
            typeof(ItemExistsValidator),
            typeof(OwnerOrAdministratorValidator)
        },
        fieldId: "bookId")
    ]
    public class AuthorBookChapterController : BaseUserController
    {

        private readonly IBookChapterService _chapterSvc;

        public AuthorBookChapterController(
            UserManager<BrUser> userManager,
            IBookChapterService chapterSvc

            ) : base(userManager)
        {
            _chapterSvc = chapterSvc;
        }

        [HttpPost("reorder")]
        public ActionResult Reorder([FromRoute] Guid bookId, [FromBody] IEnumerable<BookChapterReorderRequest> order)
        {
            var result = _chapterSvc.Reorder(bookId, order, BrUser);
            return StandardReturn(result);
        }

        [HttpPost]
        public ActionResult<IOperationResult<BookChapter>> Post([FromRoute] Guid bookId, [FromBody] BookChapterRequest item)
        {
            var check = CheckWrongId<BookChapter>(item, Guid.Empty);
            if (check != null)
            {
                return check;
            }

            var result = _chapterSvc.Add(bookId, item, BrUser);

            return StandardReturn(result);
        }

        [HttpPut("{chapterId}")]
        public ActionResult<IOperationResult<BookChapter>> Put([FromRoute] Guid bookId, [FromRoute]Guid chapterId, [FromBody] BookChapterRequest item)
        {
            var check = CheckWrongId<BookChapter>(item, chapterId);
            if (check != null)
            {
                return check;
            }

            var result = _chapterSvc.Update(bookId, item, BrUser);

            return StandardReturn(result);
        }


        [HttpDelete("{chapterId}")]
        public ActionResult<IOperationResult<BookChapter>> Delete([FromRoute] Guid bookId, [FromRoute]Guid chapterId)
        {
            var result = _chapterSvc.Delete(bookId, chapterId, BrUser);

            return StandardReturn(result);
        }

    }
}
