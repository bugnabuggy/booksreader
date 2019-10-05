using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using BooksReader.Configuration;
using BooksReader.Core.Entities;
using BooksReader.Infrastructure.DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace BooksReader.TestData.Helpers
{
    public class DatabaseDiBootstrapperInMemory : DatabaseDiBootstrapper, IServiceProviderBootstrapper
    {
        public string DbName { get; }

        public DatabaseDiBootstrapperInMemory(string dbName = "TestInMemory")
        {
            DbName = dbName;
        }

        public ServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();
            
            services.AddEntityFrameworkInMemoryDatabase()
                .AddDbContext<BrDbContext>(options => options.UseInMemoryDatabase(DbName));

            services = this.InitServiceProvider(services);
            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

        public async Task<ServiceProvider> GetServiceProviderWithSeedDb()
        {
            var provider = GetServiceProvider();
            var context = provider.GetRequiredService<BrDbContext>();
            context.ChangeTracker.LazyLoadingEnabled = false;
            context.ChangeTracker.AutoDetectChangesEnabled = false;

            var dbSeed = new TestDbContextInitializer();
            await dbSeed.SeedData(provider);

            // clear records tracking 
            DetatchEntities(context);

            return provider;
        }
    }
}
