using System;
using System.Linq;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Core.Exceptions;
using BooksReader.Infrastructure.Repositories;
using BooksReader.Infrastructure.Services;
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
		private BooksService bookService;

		[OneTimeSetUp]
		public async Task Start()
		{
			services = await new DatabaseDiBootstrapperInMemory().GetServiceProviderWithSeedDB();
			// services = await new DatabaseDiBootstrapperSQLServer().GetServiceProviderWithSeedDB();
			bookRepo = services.GetService<IRepository<Book>>();
			usersRepo = services.GetService<IRepository<BrUser>>();
			bookService = new BooksService(bookRepo, usersRepo);			
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
			var bookInrepo = bookRepo.Data.FirstOrDefault(b=>b.Id.Equals(book.Id));

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
			var books = bookService.Get(Guid.Empty.ToString());

			Assert.AreEqual(books.Count(), 1);
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

			var result = bookService.Edit(book);
			var bookInrepo = bookRepo.Data.FirstOrDefault(b => b.Id.Equals(book.Id));


			Assert.AreEqual(book.Title, bookInrepo.Title);
		}

		[Test]
		public void Should_delete_a_book()
		{
			var booksCount = bookRepo.Data.Count();

			var result = bookService.Delete("2325a096-1edc-4015-986b-111111111111");

			Assert.AreEqual(booksCount - 1, bookRepo.Data.Count());

		}
	}
}
