using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Infrastructure.Configuration;
using BooksReader.Infrastructure.DataContext;
using BooksReader.Infrastructure.Repositories;
using BooksReader.Web.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BooksReader.TestData.Helpers
{
    class TestDbContextInitializer
    {

        private Random _random = new Random();

        public async Task SeedData(IServiceProvider services)
        {
            AppConfigurator.InitRolesAndUsers(services);

            var context = services.GetService<BrDbContext>();
            var userManager = services.GetService<UserManager<BrUser>>();
			var booksRepo = services.GetService<IRepository<Book>>();

			await AddUsers(userManager);
	        await AddBooks(booksRepo);
        }

        private static async Task AddUsers(UserManager<BrUser> manager)
        {
            var users = BrUsersList.GetUsers();

            foreach (var user in users)
            {
                var brUser = new BrUser()
                {
					Id = user.Id,
                    Name = user.Username,
                    UserName = user.Username
                };
                await manager.CreateAsync(brUser, user.Password);
                await manager.AddToRolesAsync(brUser, user.Roles);
            }
        }

	    private static async Task AddBooks(IRepository<Book> booksRepo)
	    {
		   await booksRepo.AddAsync(BooksList.GetBooks());
	    }
    }
}
