using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Services;
using BooksReader.Infrastructure.Models;
using BooksReader.Infrastructure.Repositories;
using BooksReader.Infrastructure.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;


namespace BooksReader.Infrastructure.Configuration
{
    public class AppConfigurator
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IUsersService, UsersService>();
	        services.AddTransient<IRepository<LoginHistory>, DbRepository<LoginHistory>>();
			services.AddTransient<IRepository<Book>, DbRepository<Book>>();
	        services.AddTransient<IBooksService, BooksService>();
		}

        public static void InitRolesAndUsers(IServiceProvider services)
        {
            var roles = new List<IdentityRole>
            {
                new IdentityRole(SiteRoles.Admin),
                new IdentityRole(SiteRoles.User),
	            new IdentityRole(SiteRoles.Author)
			};

            var users = new Dictionary<BrUser, string>()
            {
                {new BrUser(){ UserName = "admin", Name = "admin"}, "123"},
            };

            var roleManager = services.GetRequiredService<RoleManager<IdentityRole>>();
            var userManager = services.GetRequiredService<UserManager<BrUser>>();
            var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("AppConfigurator");
            var config = services.GetRequiredService<IConfigurationRoot>();

            foreach (var role in roles)
            {
                if (!roleManager.RoleExistsAsync(role.Name).Result)
                {
                    roleManager.CreateAsync(role).Wait(Constants.AsyncTaskWaitTime);
                }
            }

            foreach (var user in users)
            {
                if (!userManager.Users.Any(u => u.UserName.Equals(user.Key.UserName)))
                {
                    var task = userManager.CreateAsync(user.Key, user.Value);
                    task.Wait(Constants.AsyncTaskWaitTime);
                    var result = task.Result;
                    if (!result.Succeeded)
                    {
                        logger.LogError(string.Join("\n", result.Errors.Select(x => x.Description)));
                    }
					userManager.AddToRolesAsync(user.Key, roles.Select(x=>x.Name)).Wait(Constants.AsyncTaskWaitTime);
                }
            }

            bool.TryParse(config["ResetAdminPassword"], out bool resetPassword);

            if (resetPassword)
            {
                var admin = userManager.FindByNameAsync("admin").Result;
                var token = userManager.GeneratePasswordResetTokenAsync(admin).Result;
                var result = userManager.ResetPasswordAsync(admin, token, config["NewPassword"] ?? "Pasword@123").Result;
            }
        }
    }
}
