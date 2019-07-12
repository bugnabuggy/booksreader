using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Enums;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Services;
using BooksReader.Core.Services.Author;
using BooksReader.Infrastructure.Repositories;
using BooksReader.TestData.Helpers;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace BooksReader.Services.Tests
{
    [TestFixture()]
    public class PublicService
    {
        private IServiceProvider _provider;
        private IPublicService _publicService;
        private IRepository<PersonalPage> _pages;


        [SetUp]
        public async Task SetUp()
        {
            _provider = await new DatabaseDiBootstrapperInMemory().GetServiceProviderWithSeedDB();
            //_provider = await new DatabaseDiBootstrapperSQLServer().GetServiceProviderWithSeedDB();

            _publicService = _provider.GetRequiredService<IPublicService>();
        }
        
        [Test]
        public void Should_get_custom_page_content_by_domain()
        {
            var requestData = new PublicPageInfoRequest()
            {
                Domain = "localhost:4200",
            };

            var info = _publicService.GetInfo(requestData);
            Assert.IsNotEmpty(info.Content);

            requestData.Domain = null;

            info = _publicService.GetInfo(requestData);
            Assert.IsNull(info);

        }
    }
}
