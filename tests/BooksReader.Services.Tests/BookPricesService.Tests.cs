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
        public void Should_add_new_book_prices()
        {

        }

    }
}
