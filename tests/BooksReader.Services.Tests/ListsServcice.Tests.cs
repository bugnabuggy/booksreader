using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Services;
using BooksReader.TestData.Helpers;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace BooksReader.Services.Tests
{

    [TestFixture]
    public class ListsServciceTests
    {
        private ServiceProvider services;
        private IListsService listsService;


        [OneTimeSetUp]
        public async Task SetUp()
        {
            services = await new DatabaseDiBootstrapperInMemory().GetServiceProviderWithSeedDB();
            listsService = services.GetRequiredService<IListsService>();
        }

        [Test]
        public void Should_get_all_lists_with_values()
        {
            var lists = listsService.GetLists().ToList();

            Assert.IsTrue(lists.Any());

            var firstList = lists.FirstOrDefault();
            Assert.IsTrue(firstList.Values.Any());
        }

        [Test]
        public void Should_get_all_lists_without_values_if_flag_is_false()
        {
            var lists = listsService.GetLists(false).ToList();

            Assert.IsTrue(lists.Any());

            var firstList = lists.FirstOrDefault();
            Assert.IsNull(firstList.Values);
        }

    }
}
