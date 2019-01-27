using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Infrastructure.Configuration;
using BooksReader.Infrastructure.DataContext;
using BooksReader.Infrastructure.Models;
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

            await AddUsers(userManager);
           
        }

        private static async Task AddUsers(UserManager<BrUser> manager)
        {
            var users = BrUsersList.GetUsers();

            foreach (var user in users)
            {
                var brUser = new BrUser()
                {
                    Name = user.Username,
                    UserName = user.Username
                };
                await manager.CreateAsync(brUser, user.Password);
                await manager.AddToRolesAsync(brUser, user.Roles);
            }
        }
    }
}
