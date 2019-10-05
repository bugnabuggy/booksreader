using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;

namespace BooksReader.Infrastructure.SeedData
{
    public class SeedTypesLists
    {
        private static readonly TypesList[] typesLists =
        {
            new TypesList{ Id = 1, Name = "Currency", LocalizationKey = "TYPE_CURRENCY" }
        };

        public static TypesList[] GetTypesLists()
        {
            return typesLists;
        }
    }
}
