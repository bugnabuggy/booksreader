using System;
using System.Linq;
using BooksReader.Core;
using BooksReader.Core.Infrastrcture;

namespace BooksReader.Utilities
{
    public class PaginationHelper
    {
        public static IQueryable<T> GetPaged<T>(IQueryable<T> data, IPaginationFilter filters, out int totalRecords)
        {
            totalRecords = data.Count();
            var pageSize = filters.PageSize ?? Constants.DefaultPageSize;

            var numberOfPages = (int)Math.Ceiling((double)totalRecords / pageSize);

            var pageNumber = !filters.PageNumber.HasValue
                ? 1
                : filters.PageNumber.Value > numberOfPages
                    ? numberOfPages
                    : filters.PageNumber.Value;

            var query = data
                .Skip(pageSize * (pageNumber - 1))
                .Take(pageSize);

            return query;
        }
    }
}
