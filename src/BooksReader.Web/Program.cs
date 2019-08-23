using System;
using BooksReader.Infrastructure.Configuration;
using BooksReader.Infrastructure.DataContext;
using BooksReader.Web.Configuration;
using IdentityServer4.EntityFramework.DbContexts;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;

namespace BooksReader.Web
{
	public class Program
	{
		public static void Main(string[] args)
		{
			var webHost = BuildWebHost(args);

			using (var scope = webHost.Services.CreateScope())
			{
				var services = scope.ServiceProvider;

				try
				{
					var db = services.GetRequiredService<BrDbContext>();
					var idsDb = services.GetRequiredService<PersistedGrantDbContext>();

                    // probably should disable automatic migrations when go to prod
					db.Database.Migrate();
					idsDb.Database.Migrate();

					AppConfigurator.InitRolesAndUsers(services);
                    AppConfigurator.InitTypesLists(services);
                }
				catch (Exception ex)
				{
					var logger = services.GetRequiredService<ILogger<Program>>();
					logger.LogError(ex, "An error occurred while migrating and initialization of the database.");
				}
			}

			webHost.Run();
		}

		public static IWebHost BuildWebHost(string[] args) =>
			WebHost.CreateDefaultBuilder(args)
                .UseUrls("https://*:5001;http://*:5000")
                .UseStartup<Startup>()
				.Build();
	}
}
