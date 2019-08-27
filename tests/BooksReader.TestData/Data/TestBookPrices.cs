using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Infrastructure.SeedData;

namespace BooksReader.TestData.Data
{
    public class TestBookPrices
    {
        public static IEnumerable<BookPrice> GetBookPrices()
        {
            return new List<BookPrice>()
            {
                new BookPrice()
                {
                    BookId = Guid.Parse("B0000000-0000-0000-0000-000000000001"),
                    CurrencyId = SeedTypeValues.GetTypeValues()[2].Id, // RUB
                    Price = 900,
                },
                new BookPrice()
                {
                    BookId = Guid.Parse("B0000000-0000-0000-0000-000000000001"),
                    CurrencyId = SeedTypeValues.GetTypeValues()[1].Id, // USD
                    Price = 20,
                },
                new BookPrice()
                {
                    BookId = Guid.Parse("2325a096-1edc-4015-986b-111111111111"),
                    CurrencyId = SeedTypeValues.GetTypeValues()[0].Id, // EUR
                    Price = 10,
                },
            };
        }
    }
}
