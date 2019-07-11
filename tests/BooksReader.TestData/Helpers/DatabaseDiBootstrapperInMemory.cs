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
        public string DbName { get; }

        public DatabaseDiBootstrapperInMemory(string dbName = "TestInMemory")
        {
            DbName = dbName;
        }

        public BrDbContext GetDataContext()
        {
            var optionsBuilder = new DbContextOptionsBuilder<BrDbContext>();
            optionsBuilder.UseInMemoryDatabase(DbName);
            var ctx = new BrDbContext(optionsBuilder.Options);
            return ctx;
        }

        public ServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddLogging();
            services.AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<BrDbContext>(options => options.UseInMemoryDatabase(DbName));

            services.AddIdentity<BrUser, IdentityRole<Guid>>(opt =>
                {
                    opt.Password.RequireDigit = false;
                    opt.Password.RequireLowercase = false;
                    opt.Password.RequiredLength = 3;
                    opt.Password.RequireNonAlphanumeric = false;
                    opt.Password.RequireUppercase = false;
                })
                .AddEntityFrameworkStores<BrDbContext>();

            AppConfigurator.ConfigureServices(services);

            var configMock = TestDbContextInitializer.GetConfigurationMock();

            services.AddSingleton<IConfigurationRoot>(c => configMock.Object);
            services.AddSingleton<IConfiguration>(c => configMock.Object);

            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

        public async Task<ServiceProvider> GetServiceProviderWithSeedDB()
        {
            var provider = GetServiceProvider();
            var context = provider.GetRequiredService<BrDbContext>();
            context.ChangeTracker.LazyLoadingEnabled = false;
            context.ChangeTracker.AutoDetectChangesEnabled = false;

            var dbSeed = new TestDbContextInitializer();
            await dbSeed.SeedData(provider);

            context.ChangeTracker.AcceptAllChanges();
            var entities = context.ChangeTracker.Entries().ToList();
            foreach (var entityEntry in entities)
            {
                entityEntry.State = EntityState.Detached;
            }

            return provider;
        }

    }
}
