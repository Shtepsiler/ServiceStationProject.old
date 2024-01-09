using IdentityServer4.Test;
using IdentityServer4.Models;
using MechanicIdentiyServer.Data;
namespace MechanicIdentiyServer.Config
{
    public class Config
    {
        public static IEnumerable<Mechanic> Clients =>
new Mechanic[]
{
new Mechanic
{
ClientId = "mechanicClient",
AllowedGrantTypes = GrantTypes.ClientCredentials,
ClientSecrets =
{
new Secret("secret".Sha256())
},
AllowedScopes = { "MechanicAPI " }
}
};
        public static IEnumerable<ApiScope> ApiScopes =>
        new ApiScope[]
        {
new ApiScope("MechanicAPI", "Mechanic API")
        };
        public static IEnumerable<ApiResource> ApiResources =>
         new ApiResource[]
         {
         };
        public static IEnumerable<IdentityResource> IdentityResources =>
         new IdentityResource[]
         {
         };
        public static List<TestUser> TestUsers =>
         new List<TestUser>
         {
         };
    }
}
