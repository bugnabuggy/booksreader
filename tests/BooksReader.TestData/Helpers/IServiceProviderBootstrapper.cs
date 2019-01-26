using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Infrastructure.DataContext;
using Microsoft.Extensions.DependencyInjection;

namespace BooksReader.TestData.Helpers
{
    interface IServiceProviderBootstrapper
    {
        BrDbContext GetDataContext();

        ServiceProvider GetServiceProvider();
        Task<ServiceProvider> GetServiceProviderWithSeedDB();
    }
}
