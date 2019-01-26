using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Infrastructure.Configuration;
using BooksReader.Infrastructure.DataContext;
using BooksReader.Infrastructure.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Moq;

namespace BooksReader.TestData.Helpers
{
    public class DatabaseDiBootstrapperSQLServer : IServiceProviderBootstrapper
    {
        private static object _contextLock = new object();
        private static bool _contextInitialized = false;
        private static bool _isDataSeeded = false;
        private static int _contextCount = 0;
        private static DbContextOptions<BrDbContext> _options;

        static DatabaseDiBootstrapperSQLServer()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BrDbContext>();
            optionsBuilder.UseSqlServer(HardcoddedConfig.ConnectionString);
            _options = optionsBuilder.Options;

            var ctx = new BrDbContext(_options);

            lock (_contextLock)
            {
                if (!_contextInitialized)
                {
                    _contextInitialized = true;
                    ctx.Database.EnsureDeleted();
                    ctx.Database.EnsureCreated();
                }
            }

        }

        public BrDbContext GetDataContext()
        {
            var ctx = new BrDbContext(_options);

            if (_contextCount < 1)
            {
                //clean befor new test session starts
                //ctx.Database.EnsureDeleted();
                //ctx.Database.EnsureCreated();
            }

            _contextCount++;
            return ctx;
        }

        public ServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddDbContext<BrDbContext>(options => options.UseSqlServer(HardcoddedConfig.ConnectionString));

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
            lock (_contextLock)
            {
                if (!_isDataSeeded)
                {
                    _isDataSeeded = true;
                    var dbSeed = new TestDbContextInitializer();
                    dbSeed.SeedData(provider).Wait(HardcoddedConfig.AsyncOperationWaitTime);
                }
            }
            return provider;
        }
    }
}
