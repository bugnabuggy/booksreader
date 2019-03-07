using System.Net.Http;
using BooksReader.Web.IdentityServerExtensions.ExtensionGrant;
using BooksReader.Web.IdentityServerExtensions.Interfaces;
using BooksReader.Web.IdentityServerExtensions.Interfaces.Processors;
using BooksReader.Web.IdentityServerExtensions.Interfaces.Repositories;
using BooksReader.Web.IdentityServerExtensions.Processors;
using BooksReader.Web.IdentityServerExtensions.Providers;
using BooksReader.Web.IdentityServerExtensions.Repositories;
using IdentityServer4.Validation;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;

namespace BooksReader.Web.IdentityServerExtensions.Extensions
{
    public static class ServiceCollectionExtensions
    {
        public static IServiceCollection AddIdentityServerExtensionsServices<TUser>(this IServiceCollection services) where TUser:IdentityUser,new()
        {
            services.AddScoped<INonEmailUserProcessor, NonEmailUserProcessor<TUser>>();
            services.AddScoped<IEmailUserProcessor, EmailUserProcessor<TUser>>();
            services.AddScoped<IExtensionGrantValidator, ExternalAuthenticationGrant<TUser>>();
            services.AddSingleton<HttpClient>();
            return services;
        }

        public static IServiceCollection AddIdentityServerExtensionsRepositories(this IServiceCollection services)
        {
            services.AddScoped<IProviderRepository, ProviderRepository>();
            return services;
        }

        public static IServiceCollection AddIdentityServerExtensionsProviders<TUser>(this IServiceCollection services) where TUser: IdentityUser,new()
        {
            services.AddTransient<IFacebookAuthProvider, FacebookAuthProvider<TUser>>();
            services.AddTransient<ITwitterAuthProvider, TwitterAuthProvider<TUser>>();
            services.AddTransient<IGoogleAuthProvider, GoogleAuthProvider<TUser>>();
            services.AddTransient<ILinkedInAuthProvider, LinkedInAuthProvider<TUser>>();
            services.AddTransient<IGitHubAuthProvider, GitHubAuthProvider<TUser>>();
            return services;
        }
    }
}
