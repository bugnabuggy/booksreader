﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using IdentityModel;
using IdentityServer4;
using IdentityServer4.Models;

namespace BooksReader.Web.Configuration
{
    public class IdServerConfig
    {
        public const string ApiName = "api";

        public static IEnumerable<ApiResource> GetApiResources()
        {
            return new List<ApiResource>
            {
                new ApiResource(ApiName, "API for BooksReader site")
                {
                    UserClaims = { JwtClaimTypes.Name, JwtClaimTypes.Role}
                }
            };
        }

        public static IEnumerable<IdentityResource> GetIdentityResources()
        {
            return new List<IdentityResource>()
            {
                new IdentityResources.Profile(),
                
            };
        }

        public static IEnumerable<Client> GetClients()
        {
            return new List<Client>
            {
                new Client
                {
                    ClientId = "mvc",
                    ClientName = "MVC Client",
                    AllowedGrantTypes = GrantTypes.ResourceOwnerPassword,
                                     
                    AccessTokenType = AccessTokenType.Jwt,
                    AccessTokenLifetime = 60*60*24*30,

                    RequireConsent = false,

                    ClientSecrets =
                    {
                        new Secret("secret".Sha256())
                    },

                    AllowedScopes =
                    {
                        ApiName
                        
                    },
                    AllowOfflineAccess = true
                }
            };
        }
    }
}