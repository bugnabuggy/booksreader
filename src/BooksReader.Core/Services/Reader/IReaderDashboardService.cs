using System;
using System.Collections.Generic;
using System.Text;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.DTO.Reader;
using BooksReader.Core.Models.Requests.Reader;

namespace BooksReader.Core.Services.Reader
{
    public interface IReaderDashboardService
    {
        IWebResult<IEnumerable<ReaderDashboardBookDto>> GetReaderBooks(ReaderDashboardFilters filters, BrUser user);
        IOperationResult<object> RemoveSubscription(Guid bookId, BrUser user);
    }
}
