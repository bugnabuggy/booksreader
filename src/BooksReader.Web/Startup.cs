using System;
using System.Reflection;
using System.Threading.Tasks;
using BooksReader.Core.Entities;
using BooksReader.Infrastructure.Configuration;
using BooksReader.Infrastructure.DataContext;
using BooksReader.Web.Configuration;
using BooksReader.Web.Filters;
using BooksReader.Web.Helpers;
using BooksReader.Web.Hubs;
using BooksReader.Web.IdentityServerExtensions.Extensions;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.SpaServices.AngularCli;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Newtonsoft.Json;
using Ruteco.AspNetCore.Translate;

namespace BooksReader.Web
{
	public class Startup
	{
		public IConfigurationRoot Configuration { get; }
		private IHostingEnvironment _environment;

		public Startup(IHostingEnvironment env)
		{
			_environment = env;

			var builder = new ConfigurationBuilder()
				.SetBasePath(env.ContentRootPath)
				.AddJsonFile("appsettings.json", optional: true, reloadOnChange: true)
				.AddJsonFile($"appsettings.{env.EnvironmentName}.json", optional: true)
				.AddJsonFile($"appsettings.private.json", optional: true);

			builder.AddEnvironmentVariables();
			Configuration = builder.Build();
		}

		// This method gets called by the runtime. Use this method to add services to the container.
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_1);
			services.AddSingleton<IConfigurationRoot>(Configuration);

			services.AddDbContext<BrDbContext>(options =>
				options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));

			services.AddMvcCore(
                    options => { options.Filters.Add(typeof(UserActionFilterAttribute)); })
				.AddFormatterMappings()
				.AddCacheTagHelper()
                .AddJsonOptions(opt=>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
				.AddJsonFormatters()
				.AddCors()
				.AddAuthorization(opt =>
				{
				});

			services.AddIdentity<BrUser, IdentityRole<Guid>>(opts =>
				{
					opts.Password = new PasswordOptions()
					{
						RequiredLength = 3,
						RequireDigit = false,
						RequireLowercase = false,
						RequireUppercase = false,
						RequireNonAlphanumeric = false
					};
				})
				.AddEntityFrameworkStores<BrDbContext>()
				.AddDefaultTokenProviders();

			services.AddIdentityServer(
                    opt=>
                    {
                        var isParsed = bool.TryParse(Configuration["Security:UseIssuerInsteadOfURL"], out bool useIssuer);
                        if (isParsed && useIssuer)
                        {
                            opt.IssuerUri = Configuration["Security:IssuerName"];
                        }
                        
                    })
				.AddDeveloperSigningCredential()
				.AddInMemoryApiResources(IdServerConfig.GetApiResources())
                .AddInMemoryIdentityResources(IdServerConfig.GetIdentityResources())
                .AddInMemoryClients(IdServerConfig.GetClients())
				.AddAspNetIdentity<BrUser>()
				.AddOperationalStore(options =>
				{
					options.ConfigureDbContext = builder =>
						builder.UseSqlServer(Configuration.GetConnectionString("DefaultConnection"),
							sql => sql.MigrationsAssembly(typeof(BrDbContext).GetTypeInfo().Assembly.GetName().Name));

					// this enables automatic token cleanup. this is optional.
					options.EnableTokenCleanup = true;
					options.TokenCleanupInterval = 30;
				});

            // IdentityServer Extensions config
            services
                .AddIdentityServerExtensionsServices<BrUser>()
                .AddIdentityServerExtensionsRepositories()
                .AddIdentityServerExtensionsProviders<BrUser>();

            services.AddAuthentication(o =>
				{
					o.DefaultScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
					o.DefaultAuthenticateScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
					o.DefaultChallengeScheme = IdentityServerAuthenticationDefaults.AuthenticationScheme;
                    
				})
                .AddFacebook(facebookOptions =>
                {
                    facebookOptions.AppId = Configuration["Authentication:Facebook:AppId"];
                    facebookOptions.AppSecret = Configuration["Authentication:Facebook:AppSecret"];
                })
                .AddIdentityServerAuthentication( options =>
                {
                    var isParsed = bool.TryParse(Configuration["Security:UseIssuerInsteadOfURL"], out bool useIssuer);
                    if (isParsed && useIssuer)
                    {
                        options.ClaimsIssuer = Configuration["Security:IssuerName"];
                    }
                    
                    options.Authority = Configuration["Security:ServerUrl"];
                    options.RequireHttpsMetadata = false;
				    options.TokenRetriever = CustomTokenRetriever.FromHeaderAndQueryString;
                    

                    options.ApiName = IdServerConfig.ApiName;
				});

			services.ConfigureApplicationCookie(options =>
			{
				options.Events.OnRedirectToLogin = context =>
				{
					context.Response.StatusCode = 401;
					return Task.CompletedTask;
				};
			});

			services.AddSignalR();

			// In production, the Angular files will be served from this directory
			services.AddSpaStaticFiles(configuration =>
			{
				configuration.RootPath = "ClientApp/dist";
			});


            // Define translations directory and set up translations
            var dictionariesLocation = System.IO.Directory.GetCurrentDirectory() + Configuration["TranslationsDictionariesLocation"];
            services.AddTranslations(dictionariesLocation);

            AppConfigurator.ConfigureServices(services);
		}

		// This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
		public void Configure(IApplicationBuilder app, IHostingEnvironment env)
        {
            app.UseCors(builder =>
            {
                builder.AllowAnyOrigin();
                builder.AllowAnyMethod();
                builder.AllowAnyHeader();
                builder.AllowCredentials();
            });

            if (env.IsDevelopment())
			{
				app.UseDeveloperExceptionPage();
			}
			else
			{
				app.UseExceptionHandler("/Error");
			}

			//app.UseSpaStaticFiles();

			
			app.UseIdentityServer();
			
			app.UseStaticFiles();
			app.UseMvc(routes =>
			{
				routes.MapRoute(
					name: "api",
					template: "api/{controller=Main}");
			});

		    app.UseSignalR(routes =>
		    {
		        routes.MapHub<UserHub>("/hub/user");
		    });

            app.UseSpa(spa =>
           {
                // To learn more about options for serving an Angular SPA from ASP.NET Core,
                // see https://go.microsoft.com/fwlink/?linkid=864501

                spa.Options.SourcePath = "ClientApp";

               if (env.IsDevelopment())
               {
                   spa.UseAngularCliServer(npmScript: "start");
               }
           });
        }
	}
}
