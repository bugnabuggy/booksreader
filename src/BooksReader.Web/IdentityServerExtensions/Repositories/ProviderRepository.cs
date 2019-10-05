using System.Collections.Generic;
using BooksReader.Web.IdentityServerExtensions.Entities;
using BooksReader.Web.IdentityServerExtensions.Helpers;
using BooksReader.Web.IdentityServerExtensions.Interfaces.Repositories;

namespace BooksReader.Web.IdentityServerExtensions.Repositories
{
    public class ProviderRepository : IProviderRepository
    {

        public  IEnumerable<Provider> Get()
        {
            return ProviderDataSource.GetProviders();
        }
    }
}
