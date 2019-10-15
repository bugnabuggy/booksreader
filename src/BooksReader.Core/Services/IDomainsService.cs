using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Models.Requests.Admin;

namespace BooksReader.Core.Services
{
    public interface IDomainsService
    {
        IOperationResult<IEnumerable<UserDomain>> Get(AllDomainsFilters filters);

        IOperationResult<UserDomainDto> Add(UserDomainRequest domain, BrUser actingUser);
        IOperationResult<UserDomainDto> Update(UserDomainRequest domain, BrUser actingUser);

        IOperationResult<UserDomainDto> Delete(Guid id, BrUser actingUser);
    }
}
