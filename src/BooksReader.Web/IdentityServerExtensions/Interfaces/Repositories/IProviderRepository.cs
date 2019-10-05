using System.Collections.Generic;
using BooksReader.Web.IdentityServerExtensions.Entities;

namespace BooksReader.Web.IdentityServerExtensions.Interfaces.Repositories
{
    public interface IProviderRepository
    {
        IEnumerable<Provider> Get();
        

    }
}
