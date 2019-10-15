using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;

namespace BooksReader.Core.Services
{
    public interface IPublicPagesService
    {
        IOperationResult<PublicPage> Get(Guid id);
        IOperationResult<PublicPage> Add(PublicPage page, BrUser actingUser);
        IOperationResult<PublicPage> Update(PublicPage page, BrUser actingUser);
        IOperationResult<PublicPage> Delete(Guid pageId, BrUser actingUser);
    }
}
