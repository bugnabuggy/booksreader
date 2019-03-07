using System;
using System.Linq;
using System.Net.Http;
using BooksReader.Web.IdentityServerExtensions.Entities;
using BooksReader.Web.IdentityServerExtensions.Helpers;
using BooksReader.Web.IdentityServerExtensions.Interfaces;
using BooksReader.Web.IdentityServerExtensions.Interfaces.Repositories;
using Microsoft.AspNetCore.Identity;
using Newtonsoft.Json.Linq;

namespace BooksReader.Web.IdentityServerExtensions.Providers
{
    public class GitHubAuthProvider<TUser> : IGitHubAuthProvider where TUser:IdentityUser,new()
    {        
        private readonly IProviderRepository _providerRepository;
        private readonly HttpClient _httpClient;
        public GitHubAuthProvider(            
            IProviderRepository providerRepository,
            HttpClient httpClient
            )
        {            
            _providerRepository = providerRepository;
            _httpClient = httpClient;
        }
        public Provider Provider => _providerRepository.Get()
                                    .FirstOrDefault(x => x.Name.ToLower() == ProviderType.GitHub.ToString().ToLower());

        public JObject GetUserInfo(string accessToken)
        {
            if (Provider == null) throw new ArgumentNullException(nameof(Provider));

            var request = $"{Provider.UserInfoEndPoint}?access_token={accessToken}";

            _httpClient.DefaultRequestHeaders.Clear();
            _httpClient.DefaultRequestHeaders.Add("cache-control", "no-cache");                      
            _httpClient.DefaultRequestHeaders.Add("Connection", "keep-alive");
            _httpClient.DefaultRequestHeaders.Add("Authorization", "token " + accessToken);           

            try
            {
                var result = _httpClient.GetAsync(Provider.UserInfoEndPoint).Result;
                if (result.IsSuccessStatusCode)
                {
                    var infoObject = JObject.Parse(result.Content.ReadAsStringAsync().Result);
                    return infoObject;
                }
                return null;
            }
            catch (Exception)
            {

                throw;
            }
         

        }
    }
}
