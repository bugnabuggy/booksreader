using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.Requests;

namespace BooksReader.Core.Services
{
    public interface IDomainsService
    {
        IOperationResult<UserDomainDto> Add(UserDomainRequest domain, BrUser actingUser);
        IOperationResult<UserDomainDto> Delete(Guid id, BrUser actingUser);
    }
}
