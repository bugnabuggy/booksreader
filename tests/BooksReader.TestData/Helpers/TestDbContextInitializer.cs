using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Configuration;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Infrastructure.DataContext;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Moq;

namespace BooksReader.TestData.Helpers
{
    public class TestDbContextInitializer
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
            AppConfigurator.InitTypesLists(services);

            //var context = services.GetService<BrDbContext>();
            //var userManager = services.GetService<UserManager<BrUser>>();
            //var booksRepo = services.GetService<IRepository<Book>>();
            //var authorProfilesRepo = services.GetService<IRepository<AuthorProfile>>();
            //var personalPagesRepo = services.GetService<IRepository<PersonalPage>>();
            //var chaptersRepo = services.GetService<IRepository<BookChapter>>();
            //var pricesRepo = services.GetService<IRepository<BookPrice>>();


            //await AddUsers(userManager);
            //await AddPersonalPages(personalPagesRepo);
            //await AddAuthorProfiles(authorProfilesRepo);
            //await AddBooks(booksRepo);
            //await AddBookChapters(chaptersRepo);
            //await AddBookPrices(pricesRepo);
        }
    }
}
