using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;

namespace BooksReader.Core.Services
{
    public interface IAuthorProfileService
    {
        Task<OperationResult<AuthorProfile>> CreateAuthorProfile(BrUser user);
    }
}
