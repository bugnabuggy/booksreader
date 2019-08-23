using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;

namespace BooksReader.Infrastructure.SeedData
{
    public class SeedTypeValues
    {
        private static readonly TypeValue[] typeValues =
       {
            new TypeValue{ Id = 100, TypeId = 1, Name = "Euro", Value = "EUR", LocalizationKey = "CURRENCY_EUR" },
            new TypeValue{ Id = 101, TypeId = 1, Name = "United Stated dollar", Value = "USD", LocalizationKey = "CURRENCY_USD" },
            new TypeValue{ Id = 102, TypeId = 1, Name = "Russian ruble", Value = "RUB", LocalizationKey = "CURRENCY_RUB" },
            new TypeValue{ Id = 103, TypeId = 1, Name = "Danish krone", Value = "DKK", LocalizationKey = "CURRENCY_DKK" },
        };

        public static TypeValue[] GetTypeValues()
        {
            return typeValues;
        }
    }
}
