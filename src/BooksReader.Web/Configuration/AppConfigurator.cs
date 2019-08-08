using System;
using System.Collections.Generic;
using System.Linq;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastructure;
using BooksReader.Core.Services;
using BooksReader.Core.Services.Author;
using BooksReader.Infrastructure.Configuration;
using BooksReader.Infrastructure.Repositories;
using BooksReader.Infrastructure.Services;
using BooksReader.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BooksReader.Web.Configuration
{
    public class AppConfigurator
    {
        public static void ConfigureServices(IServiceCollection services)
        {
            services.AddTransient<IRepository<LoginHistory>, DbRepository<LoginHistory>>();
            services.AddTransient<IRepository<Book>, DbRepository<Book>>();
            services.AddTransient<IRepository<BrUser>, DbRepository<BrUser>>();
            services.AddTransient<IRepository<SeoInfo>, DbRepository<SeoInfo>>();
            services.AddTransient<IRepository<PersonalPage>, DbRepository<PersonalPage>>();
            services.AddTransient<IRepository<AuthorProfile>, DbRepository<AuthorProfile>>();
            services.AddTransient<IRepository<BookChapter>, DbRepository<BookChapter>>();
            

            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<IUsersService, UsersService>();
            services.AddTransient<IAuthorProfileService, AuthorProfileService>();
            services.AddTransient<IPublicService, PublicService>();

            services.AddTransient<IBooksService, BooksService>();
        }

        public static void InitRolesAndUsers(IServiceProvider services)
        {
            var roles = new List<IdentityRole<Guid>>
            {
                new IdentityRole<Guid>(SiteRoles.Admin),
                new IdentityRole<Guid>(SiteRoles.User),
                new IdentityRole<Guid>(SiteRoles.Author),
                new IdentityRole<Guid>(SiteRoles.Reader)
            };

            var users = new Dictionary<BrUser, string>()
            {
                {new BrUser() {UserName = "admin", Name = "admin"}, "123"},
            };

            var roleManager = services.GetRequiredService<RoleManager<IdentityRole<Guid>>>();
            var userManager = services.GetRequiredService<UserManager<BrUser>>();
            var logger = services.GetRequiredService<ILoggerFactory>().CreateLogger("AppConfigurator");
            var config = services.GetRequiredService<IConfigurationRoot>();

            foreach (var role in roles)
                if (!roleManager.RoleExistsAsync(role.Name).Result)
                    roleManager.CreateAsync(role).Wait(Constants.AsyncTaskWaitTime);

            foreach (var user in users)
                if (!userManager.Users.Any(u => u.UserName.Equals(user.Key.UserName)))
                {
                    var task = userManager.CreateAsync(user.Key, user.Value);
                    task.Wait(Constants.AsyncTaskWaitTime);
                    var result = task.Result;
                    if (!result.Succeeded) logger.LogError(string.Join("\n", result.Errors.Select(x => x.Description)));
                    userManager.AddToRolesAsync(user.Key, roles.Select(x => x.Name)).Wait(Constants.AsyncTaskWaitTime);
                }

            bool.TryParse(config["ResetAdminPassword"], out var resetPassword);

            if (resetPassword)
            {
                var admin = userManager.FindByNameAsync("admin").Result;
                var token = userManager.GeneratePasswordResetTokenAsync(admin).Result;
                var result = userManager.ResetPasswordAsync(admin, token, config["NewPassword"] ?? "Pasword@123")
                    .Result;
            }
        }
    }
}