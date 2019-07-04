using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Infrastructure.Configuration;
using BooksReader.Infrastructure.DataContext;
using BooksReader.Web.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace BooksReader.TestData.Helpers
{
    public class DatabaseDiBootstrapperInMemory : IServiceProviderBootstrapper
    {
        public BrDbContext GetDataContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BrDbContext>();
            optionsBuilder.UseInMemoryDatabase("TestInMemory");
            var ctx = new BrDbContext(optionsBuilder.Options);
            return ctx;
        }

        public ServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<BrDbContext>(options => options.UseInMemoryDatabase("Test"));

            services.AddIdentity<BrUser, IdentityRole>(opt =>
                {
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequiredLength = 3;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<BrDbContext>();

            AppConfigurator.ConfigureServices(services);

            services.AddSingleton<IConfiguration>(c => new Mock<IConfiguration>().Object);
            services.AddSingleton<IConfigurationRoot>(c => new Mock<IConfigurationRoot>().Object);

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

        public async Task<ServiceProvider> GetServiceProviderWithSeedDB()
        {
            var provider = GetServiceProvider();
            var dbSeed = new TestDbContextInitializer();
            await dbSeed.SeedData(provider);

            return provider;
        }

    }
}
