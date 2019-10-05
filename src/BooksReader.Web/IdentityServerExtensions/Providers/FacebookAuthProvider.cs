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
    public class FacebookAuthProvider<TUser> : IFacebookAuthProvider where TUser:IdentityUser<Guid> , new()
    {
 
        private readonly IProviderRepository _providerRepository;
        private readonly HttpClient _httpClient;
        public FacebookAuthProvider(         
            IProviderRepository providerRepository,
            HttpClient httpClient
            )
        {        
            _providerRepository = providerRepository;
            _httpClient = httpClient;
        }

        public Provider Provider => _providerRepository.Get()
                                    .FirstOrDefault(x => x.Name.ToLower() == ProviderType.Facebook.ToString().ToLower());

        public JObject GetUserInfo(string accessToken)
        {
            if(Provider == null)
            {
                throw new ArgumentNullException(nameof(Provider));
            }

            var request = new Dictionary<string, string>();

            //request.Add("fields", "id,email,name,gender,birthday");
            request.Add("fields", "id,email,name");
            request.Add("access_token", accessToken);

            var result = _httpClient.GetAsync(Provider.UserInfoEndPoint + QueryBuilder.GetQuery(request, ProviderType.Facebook)).Result;
            if (result.IsSuccessStatusCode)
            {
                var infoObject = JObject.Parse(result.Content.ReadAsStringAsync().Result);
                return infoObject;
            }

            var resutl = result.Content.ReadAsStringAsync().Result;
            return null;
        }
    }
}
