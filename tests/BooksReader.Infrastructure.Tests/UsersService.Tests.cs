using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
        }

        [OneTimeTearDown]
        public void Stop()
        {

        }

        [Test]
        public void Should_Get_Users_Records()
        {
            var userManager = services.GetService<UserManager<BrUser>>();

            var userSvc = new UsersService(userManager);

            var result = userSvc.GetUsers();

            Assert.AreEqual(1, result.Total, "Users count more than expected");
            var roles = result.Data.FirstOrDefault().Roles;
            Assert.IsTrue(roles.Count() == 3);
        }
    }
}
