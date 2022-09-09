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

    public static IEnumerable<Client> GetClients(string reactBffBaseurl)
    {
        return new List<Client>
        {  
            // React BFF client
            new Client
            {
                ClientId = "react_bff",

                ClientName = "Tweeter",

                ClientSecrets = { new Secret("secret".Sha256()) },

                AllowedGrantTypes = GrantTypes.Code,
    
                // where to redirect to after login
                RedirectUris = { $"{reactBffBaseurl}/signin-oidc" },

                FrontChannelLogoutUri = $"{reactBffBaseurl}/signout-oidc",

                // where to redirect to after logout
                PostLogoutRedirectUris = { $"{reactBffBaseurl}/signout-callback-oidc" },

                AllowedCorsOrigins = {reactBffBaseurl},

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
}
