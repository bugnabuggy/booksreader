using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using BooksReader.Core;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Services;
using BooksReader.Infrastructure.Repositories;
using BooksReader.Infrastructure.SeedData;
using BooksReader.Infrastructure.Services;
using BooksReader.Services;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace BooksReader.Configuration
{
    public class AppConfigurator
    {

        public static void ConfigureServices(IServiceCollection services)
        {
            //// repositories
            services.AddTransient<IRepository<TypesList>, DbRepository<TypesList>>();
            services.AddTransient<IRepository<TypeValue>, DbRepository<TypeValue>>();
            services.AddTransient<IRepository<LoginHistory>, DbRepository<LoginHistory>>();
            services.AddTransient<IRepository<AuthorProfile>, DbRepository<AuthorProfile>>();
            services.AddTransient<IRepository<AuthorApplication>, DbRepository<AuthorApplication>>();
            services.AddTransient<IRepository<UserDomain>, DbRepository<UserDomain>>();
            services.AddTransient<IRepository<PublicPage>, DbRepository<PublicPage>>();
            services.AddTransient<IRepository<SeoInfo>, DbRepository<SeoInfo>>();

            services.AddTransient<IRepository<BrUser>, DbRepository<BrUser>>();

            services.AddTransient<IRepository<Book>, DbRepository<Book>>();
            services.AddTransient<IRepository<BookChapter>, DbRepository<BookChapter>>();
            services.AddTransient<IRepository<BookPrice>, DbRepository<BookPrice>>();

            //// infrastructure services
            services.AddTransient<ISecurityService, SecurityService>();
            services.AddTransient<IUsersService, UsersService>();
            
            //// domain services
            services.AddTransient<IListsService, ListsService>();
            services.AddTransient<IAuthorProfileService, AuthorProfileService>();
            services.AddTransient<IDomainsService, DomainsService>();
            services.AddTransient<IPublicPagesService, PublicPageService>();
            services.AddTransient<IPublicService, PublicService>();
            services.AddTransient<IBookEditingService, BookEditingService>();

            //services.AddTransient<IBooksService, BooksService>();
            //services.AddTransient<IBookChapterService, BookChapterService>();
            //services.AddTransient<IBookPriceService, BookPriceService>();
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
            var config = services.GetRequiredService<IConfiguration>();

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

            bool.TryParse(config["Security:ResetAdminPassword"], out var resetPassword);

            if (resetPassword)
            {
                var admin = userManager.FindByNameAsync("admin").Result;
                var token = userManager.GeneratePasswordResetTokenAsync(admin).Result;
                var result = userManager
                    .ResetPasswordAsync(admin, token, config["Security:NewPassword"] ?? Constants.DefaultPassword)
                    .Result;
            }
        }

        public static void InitTypesLists(IServiceProvider services)
        {
            var listsRepo = services.GetRequiredService<IRepository<TypesList>>();
            var valuesRepo = services.GetRequiredService<IRepository<TypeValue>>();

            // check and insert list types
            var seedLists = SeedTypesLists.GetTypesLists();
            var typesIntersection = listsRepo.Data.ToList()
                .Where(x => seedLists.Any(y => y.Id.Equals(x.Id)
                                               && y.Name.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase)));

            var typesToInsert = seedLists.Where(x => typesIntersection.All(y => y.Id != x.Id));
            listsRepo.Add(typesToInsert);

            // check and insert values
            var seedValues = SeedTypeValues.GetTypeValues();
            var valuesIntersection = valuesRepo.Data.ToList()
                .Where(x => seedValues.Any(y => y.Name.Equals(x.Name, StringComparison.InvariantCultureIgnoreCase)
                                                && y.TypeId.Equals(x.TypeId)));

            var valuesToInsert = seedValues.Where(x => valuesIntersection.All(y => y.Id != x.Id));
            valuesRepo.Add(valuesToInsert);
        }
    }
}
