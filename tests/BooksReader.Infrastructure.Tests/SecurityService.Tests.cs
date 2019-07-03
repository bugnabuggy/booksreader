using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Services;
using BooksReader.Infrastructure.DataContext;
using BooksReader.Infrastructure.Models;
using BooksReader.Infrastructure.Repositories;
using BooksReader.Infrastructure.Services;
using BooksReader.TestData.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BooksReader.Infrastructure.Tests
{
    [TestFixture]
    class SecurityServiceTests
    {

        private ServiceProvider services;


        [OneTimeSetUp]
        public async Task Start()
        {
            services = await new DatabaseDiBootstrapperInMemory().GetServiceProviderWithSeedDB();
            // services = await new DatabaseDiBootstrapperSQLServer().GetServiceProviderWithSeedDB();
        }

        [OneTimeTearDown]
        public void Stop()
        {

        }

        [Test]
        public void Should_Get_Users_Records()
        {
            var context = services.GetService<BrDbContext>();
            var manager = services.GetService<UserManager<BrUser>>();
            
            var securitySvc = services.GetService<ISecurityService>();

            // 
        }

    }
}
