using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Infrastructure;
using BooksReader.Core.Models;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Services;
using BooksReader.Dictionaries.Messages;
using BooksReader.Infrastructure.Services;

namespace BooksReader.Services
{
    public class BookPriceService : CRUDService<BookPrice>, IBookPriceService
    {
        private readonly IRepository<TypeValue> _typeValuesRepo;

        private readonly IEnumerable<Func<BookPrice, BookPriceService, string>> _validations =
            new List<Func<BookPrice, BookPriceService, string>>()
            {
                // price item with not empty id exist 
                {
                    (x, svc) =>
                    {
                        if(Guid.Empty.Equals(x.Id))
                        {
                            return "";
                        }

                        return svc.Repository.Data.Any(y => y.Id.Equals(x.Id))
                            ? ""
                            : MessageStrings.BookPricesMessages.PriceItemWithSelectedIdNOtExists;
                    }
                },
                // price greater than zero
                { (x, svc) => x.Price > 0
                    ? ""
                    : MessageStrings.BookPricesMessages.PriceShouldBeGreaterThanZero
                    
                },
                // currency selection
                {
                    (x, svc) =>
                         x.CurrencyId > 0
                            ? ""
                            : MessageStrings.BookPricesMessages.PriceShouldBeGreaterThanZero
                },
                // currency check
                {
                    (x, svc) =>
                        svc._typeValuesRepo.Data.Any(y => y.Id.Equals(x.CurrencyId))
                            ? ""
                            : MessageStrings.BookPricesMessages.SelectedCurrencyDoesNotExists
                }
            };


        public BookPriceService(IRepository<BookPrice> repo,
            IRepository<TypeValue> typeValuesRepo) : base(repo)
        {
            _typeValuesRepo = typeValuesRepo;
        }

        public IEnumerable<string> Validate(BookPrice item)
        {
            var result = new List<string>();

            foreach (var validation in _validations)
            {
                var message = validation.Invoke(item, this);
                if (!string.IsNullOrEmpty(message))
                {
                    result.Add(message);
                }
            }

            return result;
        }


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

        public IQueryable<BookPrice> GetByBookId(Guid bookId)
        {
            return this.Repository.Data.Where(x => x.BookId.Equals(bookId));
        }

        public IOperationResult<IEnumerable<BookPrice>> SetPrices(IEnumerable<BookPricesRequest> prices, Guid bookId)
        {
            var idsToEdit = prices.Select(x => x.Id).Where(x => !Guid.Empty.Equals(x));

            var existingPrices = Repository.Data.Where(x => x.BookId.Equals(bookId)).ToList();

            var recordsToDelete = existingPrices.Where(x => !idsToEdit.Contains(x.Id)).ToList();
            var recordsToUpdate = new List<BookPrice>();
            var recordsToAdd = new List<BookPrice>();

            foreach (var priceRecord in prices)
            {
                var existing = existingPrices.FirstOrDefault(x => x.Id.Equals(priceRecord.Id));
                if (existing != null)
                {
                    existing.CurrencyId = priceRecord.CurrencyId;
                    existing.Price = priceRecord.Price;
                    recordsToUpdate.Add(existing);
                }

                recordsToAdd.Add(new BookPrice()
                {
                    BookId = bookId,
                    Created = DateTime.UtcNow,
                    CurrencyId = priceRecord.CurrencyId,
                    Price = priceRecord.Price,
                });
            }

            // apply changes
            Repository.Delete(recordsToDelete);
            Repository.Update(recordsToUpdate);
            Repository.Add(recordsToAdd);

            return new OperationResult<IEnumerable<BookPrice>>()
            {
                Data = recordsToAdd.Union(recordsToUpdate),
                Success = true,
                Messages = new List<string>() { $"Removed [{recordsToDelete.Count()}], " +
                                                $"Updated [{recordsToUpdate.Count}], " +
                                                $"Added [{recordsToAdd.Count}]" }
            };
        }
    }
}
