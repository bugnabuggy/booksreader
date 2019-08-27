using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastructure;
using BooksReader.Core.Services;
using Microsoft.EntityFrameworkCore;

namespace BooksReader.Services
{
    public class ListsService: IListsService
    {
        private readonly IRepository<TypesList> _listsRepo;

        public ListsService(
            IRepository<TypesList> listsRepo
            )
        {
            _listsRepo = listsRepo;
        }

        public IEnumerable<TypesList> GetLists(bool includeValues = true)
        {
            var data = _listsRepo.Data;

            data = includeValues
                ? data.Include(x => x.Values).AsNoTracking()
                : data;

            return data;
        }
    }
}
