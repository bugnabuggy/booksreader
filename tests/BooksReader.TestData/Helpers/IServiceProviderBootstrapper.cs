using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace BooksReader.TestData.Helpers
{
    interface IServiceProviderBootstrapper
    {
        ServiceProvider GetServiceProvider();
        Task<ServiceProvider> GetServiceProviderWithSeedDb();
    }
}
