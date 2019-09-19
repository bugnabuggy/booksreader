using System;
using System.Linq;
using System.Threading.Tasks;
using AutoMapper;
using BooksReader.Core.Entities;
using BooksReader.Core.Exceptions;
using BooksReader.Core.Infrastructure;
using BooksReader.Core.Models;
using BooksReader.Core.Models.Requests;
using BooksReader.Core.Services;
using BooksReader.Infrastructure.Repositories;
using BooksReader.Infrastructure.Services;
using BooksReader.TestData;
using BooksReader.TestData.Data;
using BooksReader.TestData.Helpers;
using Microsoft.Extensions.DependencyInjection;
using NUnit.Framework;

namespace BooksReader.Services.Tests
{
    [TestFixture()]
    public class BookServiceTests
    {
        private ServiceProvider services;
        private IRepository<Book> bookRepo;
        private IRepository<BrUser> usersRepo;
        private IRepository<PersonalPage> pagesRepo;
        private IRepository<BookPrice> pricesRepo;
        private IRepository<BookChapter> chaptersRepo;
        private IRepository<TypeValue> typeValuesRepo;


        private IPersonalPageService personalPageService;
        private IBooksService bookService;
        private IBookPriceService bookPriceService;


        [OneTimeSetUp]
        public async Task Start()
        {
            services = await new DatabaseDiBootstrapperInMemory().GetServiceProviderWithSeedDB();
            // services = await new DatabaseDiBootstrapperSQLServer().GetServiceProviderWithSeedDB();
            bookRepo = services.GetService<IRepository<Book>>();
            usersRepo = services.GetService<IRepository<BrUser>>();
            pagesRepo = services.GetService<IRepository<PersonalPage>>();
            pricesRepo = services.GetService<IRepository<BookPrice>>();
            chaptersRepo = services.GetService<IRepository<BookChapter>>();
            typeValuesRepo = services.GetService<IRepository<TypeValue>>();

            var mapper = services.GetService<IMapper>();

            personalPageService = new PersonalPageService(pagesRepo);
            bookPriceService = new BookPriceService(pricesRepo, typeValuesRepo);

            bookService = new BooksService(bookRepo,
                usersRepo,
                chaptersRepo,
                personalPageService,
                bookPriceService,
                mapper
                );
        }

        [OneTimeTearDown]
        public void Stop()
        {

        }

        [Test]
        public void Shoud_add_a_book()
        {
            var booksCount = bookRepo.Data.Count();

            var book = new Book()
            {
                Id = Guid.NewGuid(),
                Title = "One two",
                Author = "Test"
            };

            var result = bookService.Add(book);
            var bookInrepo = bookRepo.Data.FirstOrDefault(b => b.Id.Equals(book.Id));

            Assert.AreEqual(booksCount + 1, bookRepo.Data.Count());
            Assert.NotNull(bookInrepo);
        }

        [Test]
        public void Should_rise_exception_if_title_is_empty()
        {
            var book = new Book();

            Assert.Throws<BrBadDataException>(() =>
            {
                bookService.Add(book);
            });
        }

        [Test]
        public void Should_Get_owned_books()
        {
            var books = bookService.GetByOwnerId(Guid.Parse("00000000-0000-0000-0000-00000000000A"));
            var count = books.Count();

            Assert.AreEqual(count, 1);
        }

        [Test]
        public void Should_Edit_a_book()
        {

            var book = new Book()
            {
                Id = Guid.Parse("2325a096-1edc-4015-986b-111111111111"),
                Title = "One two",
                Author = "Test"
            };

            var bookInRepo = bookRepo.Data.FirstOrDefault(b => b.Id.Equals(book.Id));
            var result = bookService.Edit(book);
            var bookInRepoAfter = bookRepo.Data.FirstOrDefault(b => b.Id.Equals(book.Id));

            Assert.AreNotEqual(book.Title, bookInRepo.Title);
            Assert.AreEqual(book.Title, bookInRepoAfter.Title);
        }

        [Test]
        public void Should_delete_a_book()
        {
            var booksCount = bookRepo.Data.Count();

            var result = bookService.Delete(Guid.Parse("2325a096-1edc-4015-986b-111111111112"));

            Assert.AreEqual(booksCount - 1, bookRepo.Data.Count());

        }


        [Test]
        public void Should_get_full_book_info()
        {
            var id = Guid.Parse("B0000000-0000-0000-0000-000000000001");
            var result = bookService.GetFull(id);

            Assert.IsTrue(result.Success);
            Assert.AreEqual(result.Data.Chapters.Count(), 3);
            Assert.IsNotNull(result.Data.Book);
            // no personal page for this book
            Assert.IsNull(result.Data.BookPage);
        }

        [Test]
        public void Should_update_full_book_info()
        {

            var testBook = TestBooks.GetBook();
            var bookEditFullRequest = new BookEditFullRequest()
            {

                Book = new BookFormSubRequest()
                {
                    Id = testBook.Id,
                    Description = testBook.Description,
                    Title = "Changed title",
                    Author = "Changed Author",
                    IsPublished = true,
                    IsForSale = true
                },
                Prices = new[]{
                    new BookPricesRequest() {

                },},
                BookPage = new PublicPageRequest()
                {
                    
                }
                // Chapters = TestBookChapters.GetChapters()
            };

            var result = bookService.SaveFull(bookEditFullRequest);

        }
    }
}
