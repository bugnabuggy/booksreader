using BooksReader.Core.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.Requests;

namespace BooksReader.Core.Services.Author
{
    public interface IAuthorProfileService
    {
        Task<OperationResult<AuthorProfile>> CreateAuthorProfile(BrUser user);
        AuthorProfile GetAuthorProfile(BrUser user);
        OperationResult<AuthorProfile> EditAuthorProfile(AuthorProfileDto profile);
        Task<OperationResult<AuthorProfile>> DeleteAuthorProfile(AuthorProfile profile);
    }
}
