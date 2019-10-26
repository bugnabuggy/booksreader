using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using AutoMapper;
using BooksReader.Core;
using BooksReader.Core.Entities;
using BooksReader.Core.Enums;
using BooksReader.Core.Infrastrcture;
using BooksReader.Core.Models.Requests.Author;
using BooksReader.Core.Services;
using BooksReader.Dictionaries.Messages;
using Microsoft.EntityFrameworkCore;

namespace BooksReader.Services
{
    public class BookPricesService: IBookPricesService
    {
        // Validations as list of functions
        private readonly IEnumerable<Func<BookPrice, BookPricesService, BrUser, string>> _validations = 
            new List<Func<BookPrice, BookPricesService, BrUser, string>>()
            {
                // book should exists and be owned by user 
                (price, svc, user) =>
                {
                    var book = svc._booksRepo.Data.AsNoTracking()
                        .FirstOrDefault(x => x.Id.Equals(price.BookId));

                    var msg = book != null
                        ? null
                        : MessageStrings.BookPricesMessages.BookDoesNotExists;

                    if (string.IsNullOrWhiteSpace(msg))
                    {
                        return msg;
                    }

                    var isAdmin = svc._security.IsInRole(user.Id, SiteRoles.Admin);
                    var isOwner = book.OwnerId.Equals(user.Id);

                    msg = !isOwner && !isAdmin
                          ? MessageStrings.BookPricesMessages.NotAnOwnerOrAdmin
                          : null;

                    return msg;
                },

                // should not have a price for the same currency 
                (price, svc, user) =>
                {
                    var sameCurrencyPrice = svc._pricesRepo.Data.AsNoTracking()
                        .Any(x => x.BookId.Equals(price.BookId) 
                                  && x.CurrencyId == price.CurrencyId
                                  && !x.Id.Equals(price.Id));

                    var msg = sameCurrencyPrice 
                            ? MessageStrings.BookPricesMessages.PriceWithTheSameCurrencyExists
                            : null;

                    return msg;
                },

                // currency should exist
                (price, svc, user) =>
                {
                    var currencies = svc._typevaluesRepo.Data.AsNoTracking()
                        .Where(x => x.TypeId.Equals((ushort) TypeLists.Currencies));

                    var msg = currencies.Any(x=>x.Id == price.CurrencyId)
                        ? null
                        : MessageStrings.BookPricesMessages.SelectedCurrencyDoesNotExists;

                    return msg;
                },

                // price greater than zero
                (price, svc, user) =>
                {
                    var msg = price.Price > 0
                        ? null
                        : MessageStrings.BookPricesMessages.PriceShouldBeGreaterThanZero;

                    return msg;
                }
            };

        private readonly IRepository<BookPrice> _pricesRepo;
        private readonly IRepository<Book> _booksRepo;
        private readonly IRepository<TypeValue> _typevaluesRepo;
        private readonly ISecurityService _security;
        private readonly IMapper _mapper;

        public BookPricesService(
            IRepository<BookPrice> pricesRepo,
            IRepository<Book> booksRepo,
            IRepository<TypeValue> typeValuesRepo,
            ISecurityService security,
            IMapper mapper
            )
        {
            _pricesRepo = pricesRepo;
            _booksRepo = booksRepo;
            _typevaluesRepo = typeValuesRepo;
            _security = security;
            _mapper = mapper;
        }

        public IOperationResult<IEnumerable<BookPrice>> GetPrices(Guid bookId)
        {
            var prices = _pricesRepo.Data.AsNoTracking()
                .Where(x => x.BookId.Equals(bookId));

            var result = new OperationResult<IEnumerable<BookPrice>>()
            {
                Data = prices,
                Success = true
            };

            return result;
        }

        IEnumerable<string> Validate(BookPrice price, BrUser user)
        {
            var validations = this._validations.Select(x => x.Invoke(price, this, user))
                .Where(x => !string.IsNullOrWhiteSpace(x));

            return validations;
        }

        public IOperationResult<BookPrice> Add(BookPriceRequest priceData, BrUser user)
        {
            var price = _mapper.Map<BookPrice>(priceData);
            // set the owner as acting user
            price.OwnerId = user.Id;

            var validations = Validate(price, user).ToList();

            if (validations.Any())
            {
                return new OperationResult<BookPrice>()
                {
                    Data = price,
                    Messages = validations
                };
            }

            price.Created = DateTime.UtcNow;
            
            // OwnerId should be filled as part of the data

            _pricesRepo.Add(price);

            return new OperationResult<BookPrice>()
            {
                Data = price,
                Success = true
            };
        }

        public IOperationResult<BookPrice> Edit(BookPriceRequest priceData, BrUser user)
        {
            var price = _mapper.Map<BookPrice>(priceData);

            var validations = Validate(price, user).ToList();
            
            if (validations.Any())
            {
                return new OperationResult<BookPrice>()
                {
                    Data = price,
                    Messages = validations
                };
            }

            var existingPrice = _pricesRepo.Data.FirstOrDefault(x => x.Id.Equals(price.Id));

            existingPrice.CurrencyId = price.CurrencyId;
            existingPrice.Price = price.Price;
            existingPrice.Created = DateTime.UtcNow;

            _pricesRepo.Update(existingPrice);

            return new OperationResult<BookPrice>()
            {
                Data = existingPrice,
                Success = true
            };
        }

        public IOperationResult<BookPrice> Delete(Guid priceId, BrUser user)
        {
            var price = _pricesRepo.Data.FirstOrDefault(x => x.Id.Equals(priceId));

            _pricesRepo.Delete(price);

            return new OperationResult<BookPrice>()
            {
                Data = price,
                Success = true
            };
        }

    }
}
