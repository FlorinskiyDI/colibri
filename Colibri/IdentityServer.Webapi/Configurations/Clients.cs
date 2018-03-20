using IdentityServer4.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace IdentityServer.Webapi.Configurations
{
    public class Clients
    {
        public static string HOST_URL = "http://localhost:8080";

        // clients want to access resources (aka scopes)
        public static IEnumerable<Client> GetClients()
        {
            // client credentials client
            return new List<Client>
            {
                new Client
                {
                    ClientName = "singleapp",
                    ClientId = "singleapp",
                    AccessTokenType = AccessTokenType.Reference,
                    //AccessTokenLifetime = 600, // 10 minutes, default 60 minutes
                    AllowedGrantTypes = GrantTypes.Implicit,
                    RequireConsent = false,
                    AllowAccessTokensViaBrowser = true,
                    RedirectUris = new List<string>
                    {
                         HOST_URL

                    },
                    PostLogoutRedirectUris = new List<string>
                    {
                         HOST_URL
                    },
                    AllowedCorsOrigins = new List<string>
                    {
                         HOST_URL
                    },
                    AllowedScopes = new List<string>
                    {
                        "openid",
                        "dataEventRecords",
                        "dataeventrecordsscope",
                        "securedFiles",
                        "securedfilesscope",
                        "role"
                    }
                }
            };
        }

    }
}
