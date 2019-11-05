using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Enums;

namespace BooksReader.Infrastructure.SeedData
{
    public class SeedTypesLists
    {
        private static readonly TypesList[] typesLists =
        {
            new TypesList{ Id = (short)TypeLists.Currencies, Name = "Currency", LocalizationKey = "TYPE_CURRENCY" }
        };

        public static TypesList[] GetTypesLists()
        {
            return typesLists;
        }
    }
}
