using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastructure;
using BooksReader.Core.Models;
using BooksReader.Core.Services;
using BooksReader.Infrastructure.Services;

namespace BooksReader.Services
{
    public class BookPriceService : CRUDService<BookPrice>, IBookPriceService, IValidator<BookPrice>
    {

        public BookPriceService(IRepository<BookPrice> repo) : base(repo)
        {
        }

        public IEnumerable<string> Validate(BookPrice item)
        {
            throw new NotImplementedException();
        }

        public override IOperationResult<BookPrice> Add(BookPrice item)
        {
            var valdations = Validate(item);
            return null;
        }
    }
}
