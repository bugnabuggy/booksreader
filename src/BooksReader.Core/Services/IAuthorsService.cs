using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.DTO.Admin;

namespace BooksReader.Core.Services
{
    public interface IAuthorsService
    {
        IWebResult<IEnumerable<AuthorApplicationDto>> GetAuthorApplications();
    }
}
