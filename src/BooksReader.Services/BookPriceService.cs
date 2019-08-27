using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastructure;
using BooksReader.Core.Models;
using BooksReader.Core.Services;
using BooksReader.Dictionaries.Messages;
using BooksReader.Infrastructure.Services;

namespace BooksReader.Services
{
    public class BookPriceService : CRUDService<BookPrice>, IBookPriceService
    {
        private readonly IEnumerable<Expression<Func<BookPrice, string>>> _validations =
            new List<Expression<Func<BookPrice, string>>>()
            {
                {x => x.Price > 0
                    ? ""
                    : MessageStrings.BookPricesMessages.PriceShouldBeGreaterThanZero
                    
                },
                
            };


        public BookPriceService(IRepository<BookPrice> repo) : base(repo)
        {
        }

        public IEnumerable<string> Validate(BookPrice item)
        {
            var result = new List<string>();

            foreach (var validation in _validations)
            {
                var message = validation.Compile().Invoke(item);
                if (!string.IsNullOrEmpty(message))
                {
                    result.Add(message);
                }
            }

            return result;
        }

        //public override IOperationResult<BookPrice> Add(BookPrice item)
        //{
        //    var valdations = Validate(item);
        //    return null;
        //}

        public IOperationResult<IEnumerable<BookPrice>> SetPrices(IEnumerable<BookPrice> prices)
        {
            var result = new OperationResult<IEnumerable<BookPrice>>();

            foreach (var price in prices)
            {
                if (Guid.Empty.Equals(price.Id))
                {
                    Add(price);
                }
                else
                {
                    Edit(price);
                }
            }

            return result;
        }
    }
}
