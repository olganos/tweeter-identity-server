using Duende.IdentityServer;
using Duende.IdentityServer.Models;
using IdentityModel;

namespace IdentityServer;

public static class Config
{
    public static IEnumerable<IdentityResource> IdentityResources =>
        new IdentityResource[]
        {
            new IdentityResources.OpenId(),
            new IdentityResources.Profile(),
            new IdentityResource
            {
                Name = "verification",
                UserClaims = new List<string>
                {
                    JwtClaimTypes.Email,
                    JwtClaimTypes.EmailVerified
                }
            },
            new IdentityResource
            {
                Name = "fullname",
                UserClaims =  new List<string> {
                    "first_name" ,
                    "last_name"
                }
            }
        };

    public static IEnumerable<ApiScope> ApiScopes =>
       new ApiScope[]
           {
                new ApiScope(name: "tweeter-api", displayName: "Tweeter Api")
           };

    public static IEnumerable<Client> Clients =>
        new List<Client>
        {           
            // JavaScript BFF client
            new Client
            {
                ClientId = "bff",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
    
                // where to redirect to after login
                RedirectUris = { "https://localhost:5003/signin-oidc" },

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:5003/signout-callback-oidc" },

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    "verification",
                    "fullname",
                    "api1"
                }
            },
            // React BFF client
            new Client
            {
                ClientId = "react_bff",
                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
    
                // where to redirect to after login
                RedirectUris = { "https://localhost:44485/signin-oidc" },

                FrontChannelLogoutUri = "https://localhost:44485/signout-oidc",

                // where to redirect to after logout
                PostLogoutRedirectUris = { "https://localhost:44485/signout-callback-oidc" },

                AllowedCorsOrigins = {"https://localhost:44485"},

                AllowedScopes = new List<string>
                {
                    IdentityServerConstants.StandardScopes.OpenId,
                    IdentityServerConstants.StandardScopes.Profile,
                    IdentityServerConstants.StandardScopes.Email,
                    "verification",
                    "fullname",
                    "tweeter-api"
                }
            }
        };
}
