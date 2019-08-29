using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Enums;
using BooksReader.Core.Infrastructure;
using BooksReader.Core.Models;
using BooksReader.Core.Models.Requests;

namespace BooksReader.Core.Services
{
    public interface IPersonalPageService : ICRUDOperatonService<PersonalPage>, IValidator<PersonalPage>
    {
        IOperationResult<PersonalPage> Save(PublicPageRequest request, Guid subjectId, PersonalPageType type);
    }
}
