using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.DTO.Author;

namespace BooksReader.Core.Services
{
    public interface IAuthorProfileService
    {
        AuthorProfile GetAuthorProfile(Guid brUserId);
        AuthorProfileDto GetAuthorFullProfile(Guid brUserId);

        Task<OperationResult<AuthorProfile>> CreateAuthorProfile(BrUser user);
        Task<OperationResult<AuthorProfile>> EditAuthorProfile(AuthorProfileDto profile);


    }
}
