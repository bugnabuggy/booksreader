using System;
using System.Reflection;
using System.Threading.Tasks;
using AutoMapper;
using BooksReader.Configuration;
using BooksReader.Core.Entities;
using BooksReader.Infrastructure.DataContext;
using BooksReader.Web.Filters;
using BooksReader.Web.Helpers;
using BooksReader.Web.Hubs;
using BooksReader.Web.IdentityServerExtensions.Configuration;
using BooksReader.Web.IdentityServerExtensions.Extensions;
using IdentityServer4.AccessTokenValidation;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.HttpsPolicy;
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
        public Startup(IConfiguration configuration)
        {
            Configuration = configuration;
        }

        public IConfiguration Configuration { get; }

        // This method gets called by the runtime. Use this method to add services to the container.
        public void ConfigureServices(IServiceCollection services)
        {
            //services.AddMvc().SetCompatibilityVersion(CompatibilityVersion.Version_2_2);

            services.AddDbContext<BrDbContext>(options =>
                options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));


            services.AddMvcCore(
                    options => { options.Filters.Add(typeof(UserActionFilterAttribute)); })
                .AddFormatterMappings()
                .AddCacheTagHelper()
                .AddJsonOptions(opt =>
                {
                    opt.SerializerSettings.ReferenceLoopHandling = ReferenceLoopHandling.Ignore;
                })
                .AddJsonFormatters()
                .AddCors()
                .AddAuthorization(opt =>
                {
                });

            services.AddAutoMapper(typeof(AutoMapperConfig));

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
                    opt =>
                    {
                        var isParsed = bool.TryParse(Configuration["Security:UseIssuerInsteadOfURL"], out bool useIssuer);
                        if (isParsed && useIssuer)
                        {
                            opt.IssuerUri = Configuration["Security:IssuerName"];
                        }

                    })
                .AddDeveloperSigningCredential()
                .AddInMemoryApiResources(IdentityServerConfig.GetApiResources())
                .AddInMemoryIdentityResources(IdentityServerConfig.GetIdentityResources())
                .AddInMemoryClients(IdentityServerConfig.GetClients())
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
                .AddIdentityServerAuthentication(options =>
                {
                    var isParsed = bool.TryParse(Configuration["Security:UseIssuerInsteadOfURL"], out bool useIssuer);
                    if (isParsed && useIssuer)
                    {
                        options.ClaimsIssuer = Configuration["Security:IssuerName"];
                    }

                    options.Authority = Configuration["Security:ServerUrl"];
                    options.RequireHttpsMetadata = false;
                    options.TokenRetriever = CustomTokenRetriever.FromHeaderAndQueryString;


                    options.ApiName = IdentityServerConfig.ApiName;
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

            if (env.IsDevelopment() || env.IsEnvironment("Test"))
            {
                app.UseDeveloperExceptionPage();
            }
            else
            {
                app.UseExceptionHandler("/Error");
                // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
                app.UseHsts();
            }

            app.UseHttpsRedirection();

            app.UseIdentityServer();

            app.UseStaticFiles();
            app.UseSpaStaticFiles();

            

            app.UseMvc(routes =>
            {
                routes.MapRoute(
                    name: "default",
                    template: "api/{controller}/{action=Index}/{id?}");
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

                spa.UseSpaPrerendering(options =>
                {
                    options.BootModulePath = $"{spa.Options.SourcePath}/dist-server/main.js";
                    options.BootModuleBuilder = env.IsDevelopment()
                        ? new AngularCliBuilder(npmScript: "build:ssr")
                        : null;
                    options.ExcludeUrls = new[] { "/sockjs-node" };

                    options.SupplyData = ((context, data) =>
                    {
                        data["cookies"] = context.Request.Cookies;
                        data["servEnv"] = env.EnvironmentName;
                    });
                });

                if (env.IsDevelopment())
                {
                    spa.UseAngularCliServer(npmScript: "start");
                }
            });
        }
    }
}
