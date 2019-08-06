using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastructure;
using BooksReader.Core.Models.DTO;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Services;
using BooksReader.Infrastructure.Configuration;
using BooksReader.Infrastructure.DataContext;
using BooksReader.Infrastructure.Repositories;
using BooksReader.Infrastructure.Services;
using BooksReader.TestData.Data;
using BooksReader.TestData.Helpers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BooksReader.Infrastructure.Tests
{
    [TestFixture]
    public class UsersServiceTests
    {
        private ServiceProvider services;


        [SetUp]
        public async Task Start()
        {
            services = await new DatabaseDiBootstrapperInMemory().GetServiceProviderWithSeedDB();
            // services = await new DatabaseDiBootstrapperSQLServer().GetServiceProviderWithSeedDB();
        }

        [TearDown]
        public void Stop()
        {

        }

        [Test]
        public void Should_Get_Users_Records()
        {
            var context = services.GetService<BrDbContext>();
            var manager = services.GetService<UserManager<BrUser>>();
	        var repository = services.GetService<IRepository<LoginHistory>>();
            var userSvc = new UsersService(context, manager, repository);

            var result = userSvc.GetUsersWithRoles().ToList();

            Assert.AreEqual(4, result.Count, "Users count not than expected");
            var roles = result.FirstOrDefault().Roles;
            Assert.IsTrue(roles.Count() == 4);

            roles = result[1].Roles;
            Assert.IsTrue(roles.Count() == 0);
        }

        [Test]
        public async Task Should_add_user_to_role()
        {
            var context = services.GetService<BrDbContext>();
            var manager = services.GetService<UserManager<BrUser>>();
	        var repository = services.GetService<IRepository<LoginHistory>>();
			var userSvc = new UsersService(context, manager, repository);

            var result = await userSvc.AddUserRole("test", SiteRoles.Author);

            var user = await manager.FindByNameAsync("test");
            var roles = await manager.GetRolesAsync(user);

            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Messages.Count() == 0);
            Assert.IsTrue(roles.Contains(SiteRoles.Author));

            // second time
            result = await userSvc.AddUserRole("test", SiteRoles.Author);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Messages.Count() == 0);
            roles = await manager.GetRolesAsync(user);
            Assert.IsTrue(roles.Contains(SiteRoles.Author));

            // remove
            result = await userSvc.RemoveUserRole("test", SiteRoles.Author);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Messages.Count() == 0);
            roles = await manager.GetRolesAsync(user);
            Assert.IsFalse(roles.Contains(SiteRoles.Author));

            // second time
            result = await userSvc.RemoveUserRole("test", SiteRoles.Author);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Messages.Count() == 0);
            roles = await manager.GetRolesAsync(user);
            Assert.IsFalse(roles.Contains(SiteRoles.Author));

            // toggle
            result = await userSvc.ToggleUserRole("test", SiteRoles.Author);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Messages.Count() == 0);
            roles = await manager.GetRolesAsync(user);
            Assert.IsTrue(roles.Contains(SiteRoles.Author));

            // toggle
            result = await userSvc.ToggleUserRole("test", SiteRoles.Author);
            Assert.IsTrue(result.Success);
            Assert.IsTrue(result.Messages.Count() == 0);
            roles = await manager.GetRolesAsync(user);
            Assert.IsFalse(roles.Contains(SiteRoles.Author));
        }

	    [Test]
	    public async Task Should_add_login_history()
	    {
		    var context = services.GetService<BrDbContext>();
		    var manager = services.GetService<UserManager<BrUser>>();
		    var repository = services.GetService<IRepository<LoginHistory>>();
		    var userSvc = new UsersService(context, manager, repository);
		    var count = repository.Data.Count();

		    var history = new LoginHistory()
		    {

		    };

		    var result = await userSvc.AddLoginHistory(history, Guid.Empty);

			Assert.AreEqual(repository.Data.Count(), count + 1);
	    }

        [Test]
        public async Task Should_update_profile()
        {
            var context = services.GetService<BrDbContext>();
            
            var user = context.Users.AsNoTracking().FirstOrDefault() ?? new BrUser();
            var userSvc =  services.GetService<IUsersService>();

            var request = new UserProfileRequest()
            {
                Username = user.UserName,
                Avatar = $"Changed {user.Avatar}",
                Name = $"Changed {user.Name}",
                Email = $"Changed {user.Email}"
            };

            var result = await userSvc.Update(request);
            Assert.IsTrue(result.Success);

            user = context.Users.AsNoTracking().FirstOrDefault();

            Assert.AreEqual(user.Name, request.Name);
            Assert.AreEqual(user.Avatar, request.Avatar);
            Assert.AreEqual(user.Email, request.Email);
        }


        [Test]
        public async Task Should_delete_user()
        {
            var context = services.GetService<BrDbContext>();
            var userSvc = services.GetService<IUsersService>();
            var user = TestBrUsers.GetUser();

            var result = await userSvc.Delete(user.UserName);
            Assert.IsTrue(result.Success);

            
            var dbuser = context.Users.AsNoTracking().FirstOrDefault(x=>x.UserName.Equals(user.UserName));
            Assert.IsNull(dbuser);
        }
    }
}
