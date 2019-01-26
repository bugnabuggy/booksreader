using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Infrastructure.DataContext;
using BooksReader.Infrastructure.Models;
using BooksReader.Infrastructure.Services;
using BooksReader.TestData.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BooksReader.Infrastructure.Tests
{
    [TestFixture]
    public class UsersServiceTests
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

            var userSvc = new UsersService(context);

            var result = userSvc.GetUsersWithRoles().ToList();

            Assert.AreEqual(3, result.Count, "Users count not than expected");
            var roles = result.FirstOrDefault().Roles;
            Assert.IsTrue(roles.Count() == 3);
        }
    }
}
