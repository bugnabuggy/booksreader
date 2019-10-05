using System;
using System.Collections.Generic;
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
    public class GoogleAuthProvider<TUser> : IGoogleAuthProvider where TUser:IdentityUser<Guid> ,new()
    {
        
        private readonly IProviderRepository _providerRepository;
        private readonly HttpClient _httpClient;
        public GoogleAuthProvider(
             
             IProviderRepository providerRepository,
             HttpClient httpClient
             )
        {
            
            _providerRepository = providerRepository;
            _httpClient = httpClient;
        }
        public Provider Provider => _providerRepository.Get()
                                    .FirstOrDefault(x => x.Name.ToLower() == ProviderType.Google.ToString().ToLower());
        public JObject GetUserInfo(string accessToken)
        {
            var request = new Dictionary<string, string>();
            request.Add("token", accessToken);

            var result = _httpClient.GetAsync(Provider.UserInfoEndPoint + QueryBuilder.GetQuery(request, ProviderType.Google)).Result;
            if (result.IsSuccessStatusCode)
            {
                var infoObject = JObject.Parse(result.Content.ReadAsStringAsync().Result);
                return infoObject;
            }
            return null;
        }
    }
}
