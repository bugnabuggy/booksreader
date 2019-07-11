using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Services.Author;
using BooksReader.Infrastructure.Repositories;
using BooksReader.TestData.Data;
using BooksReader.TestData.Helpers;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace BooksReader.Services.Tests
{
    [TestFixture()]
    public  class AuthorProfileService
    {
        private IAuthorProfileService _authProfSvc;
        private IRepository<AuthorProfile>_authProfRepo;
        private IRepository<PersonalPage> _personalPagesRepo;
        private IServiceProvider _provider;
        
        [SetUp]
        public async Task SetUp()
        {
            _provider = await new DatabaseDiBootstrapperInMemory().GetServiceProviderWithSeedDB();
            //_provider = await new DatabaseDiBootstrapperSQLServer().GetServiceProviderWithSeedDB();
            _authProfSvc = _provider.GetRequiredService<IAuthorProfileService>();
            _authProfRepo = _provider.GetRequiredService<IRepository<AuthorProfile>>();
            _personalPagesRepo = _provider.GetRequiredService<IRepository<PersonalPage>>();
        }

        [Test]
        public async Task Should_Create_AuthorProfile_Record()
        {
            var authProfilesCount = _authProfRepo.Data.Count();
            var brUser = TestData.Data.TestBrUsers.GetUser();

            var result = await _authProfSvc.CreateAuthorProfile(brUser);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(authProfilesCount + 1, _authProfRepo.Data.Count());

        }

        [Test]
        public void Should_Edit_AuthorProfile_And_Create_Personal_Page_If_It_Was_Empty()
        {
            var authorProfileRequest = TestAuthors.GetAuthorProfileEditRequest();
            var author = _authProfRepo.Get(x => x.Id.Equals(authorProfileRequest.Id))
                .FirstOrDefault();
            // does not work properly → 
            // // var author = _authProfRepo.Data.FirstOrDefault(x => x.Id.Equals(authorProfileRequest.ProfileId));

            var personalPagesCount = _personalPagesRepo.Data.Count();

            var result = _authProfSvc.EditAuthorProfile(authorProfileRequest);

            Assert.IsTrue(result.Success);
            Assert.AreNotEqual(author.AuthorName, result.Data.AuthorName);
            Assert.AreEqual(personalPagesCount + 1, _personalPagesRepo.Data.Count());

        }

    }
}
