using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;

namespace BooksReader.Core.Services
{
    public interface IListsService
    {
        IEnumerable<TypesList> GetLists(bool includeValues = true);
    }
}
