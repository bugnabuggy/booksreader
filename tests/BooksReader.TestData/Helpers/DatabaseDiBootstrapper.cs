using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
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
    public abstract class DatabaseDiBootstrapper
    {
        public ServiceCollection InitServiceProvider(ServiceCollection services)
        {
            services.AddLogging();
            services.AddAutoMapper(typeof(AutoMapperConfig));
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

            return services;
        }

        public void DetatchEntities(BrDbContext context)
        {
            context.ChangeTracker.AcceptAllChanges();
            var entities = context.ChangeTracker.Entries().ToList();
            foreach (var entityEntry in entities)
            {
                entityEntry.State = EntityState.Detached;
            }
        }
    }
}
