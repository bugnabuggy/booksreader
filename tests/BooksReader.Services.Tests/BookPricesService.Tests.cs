using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Services;
using BooksReader.TestData.Data;
using BooksReader.TestData.Helpers;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;
using NUnit.Framework.Internal;

namespace BooksReader.Services.Tests
{
    [TestFixture()]
    public class BookPricesServiceTests
    {
        private ServiceProvider services;
        private IBookPriceService _pricesService;


        [OneTimeSetUp]
        public async Task SetUp()
        {
            services = await new DatabaseDiBootstrapperInMemory().GetServiceProviderWithSeedDB();
            _pricesService = services.GetRequiredService<IBookPriceService>();
        }

        [Test]
        public void Should_set_new_book_prices_from_dto()
        {
            var newPrices = TestBookPrices.GetNewPricesRequest();
            var bookId = Guid.Parse("B0000000-0000-0000-0000-000000000001");

            var result = _pricesService.SetPrices(newPrices, bookId);

            Assert.IsTrue(result.Success);
        }




    }
}
