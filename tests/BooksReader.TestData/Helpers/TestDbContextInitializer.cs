using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Infrastructure.DataContext;
using BooksReader.Infrastructure.Repositories;
using BooksReader.TestData.Data;
using BooksReader.Web.Configuration;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Configuration;
using Moq;

namespace BooksReader.TestData.Helpers
{
    class TestDbContextInitializer
    {

        private Random _random = new Random();

        public static Mock<IConfigurationRoot> GetConfigurationMock()
        {
            var mock = new Mock<IConfigurationRoot>();



            return mock;
        }

        public async Task SeedData(IServiceProvider services)
        {
            AppConfigurator.InitRolesAndUsers(services);

            var context = services.GetService<BrDbContext>();
            var userManager = services.GetService<UserManager<BrUser>>();
			var booksRepo = services.GetService<IRepository<Book>>();
            var authorProfilesRepo = services.GetService<IRepository<AuthorProfile>>();
            var personalPagesRepo = services.GetService<IRepository<PersonalPage>>();

            await AddUsers(userManager);
            await AddPersonalPages(personalPagesRepo);
            await AddAuthorProfiles(authorProfilesRepo);
	        await AddBooks(booksRepo);
        }

        private static async Task AddUsers(UserManager<BrUser> manager)
        {
            var users = TestBrUsers.GetUsersInfo();

            foreach (var user in users)
            {
                var brUser = new BrUser()
                {
					Id = Guid.Parse(user.Id),
                    Name = user.Username,
                    UserName = user.Username
                };
                await manager.CreateAsync(brUser, user.Password);
                await manager.AddToRolesAsync(brUser, user.Roles);
            }
        }

        private static async Task AddAuthorProfiles(IRepository<AuthorProfile> repo)
        {
            await repo.AddAsync(TestAuthors.GetAuthorProfiles());
        }

	    private static async Task AddBooks(IRepository<Book> booksRepo)
	    {
		   await booksRepo.AddAsync(TestBooks.GetBooks());
	    }

        private static async Task AddPersonalPages(IRepository<PersonalPage> pagesRepo)
        {
            await pagesRepo.AddAsync(TestPersonalPages.GetPersonalPages());
        }
    }
}
