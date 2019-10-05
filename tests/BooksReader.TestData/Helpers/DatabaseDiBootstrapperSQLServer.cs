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
    public class DatabaseDiBootstrapperSQLServer : DatabaseDiBootstrapper, IServiceProviderBootstrapper
    {
        private static readonly object _contextLock = new object();
        private static bool _isDataSeeded = false;
        

        public ServiceProvider GetServiceProvider()
        {
            var services = new ServiceCollection();
            services.AddDbContext<BrDbContext>(options => options.UseSqlServer(HardcoddedConfig.ConnectionString));

            services = this.InitServiceProvider(services);
            var serviceProvider = services.BuildServiceProvider();

            return serviceProvider;
        }

        public Task<ServiceProvider> GetServiceProviderWithSeedDb()
        {
            var provider = GetServiceProvider();
            lock (_contextLock)
            {
                if (!_isDataSeeded)
                {
                    _isDataSeeded = true;

                    var context = provider.GetRequiredService<BrDbContext>();

                    context.Database.EnsureDeleted();
                    context.Database.EnsureCreated();

                    // Dont want seed data being tracked
                    context.ChangeTracker.LazyLoadingEnabled = false;
                    context.ChangeTracker.AutoDetectChangesEnabled = false;

                    var dbSeed = new TestDbContextInitializer();
                    dbSeed.SeedData(provider).Wait(HardcoddedConfig.AsyncOperationWaitTime);

                    // Clear and detach all inserted objects 
                    DetatchEntities(context);
                }
            }
            return Task.FromResult(provider);
        }
    }
}