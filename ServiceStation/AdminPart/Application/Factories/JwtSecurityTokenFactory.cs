using Application.Configurations;
using Application.Factories.Interfaces;
using Domain.Entities;

using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace Application.Factories
{
    public class JwtSecurityTokenFactory : IJwtSecurityTokenFactory
    {
        private readonly JwtTokenConfiguration jwtTokenConfiguration;

        public JwtSecurityToken BuildToken(Manager manager) => new(
            issuer: jwtTokenConfiguration.Issuer,
            audience: jwtTokenConfiguration.Audience,
            claims: GetClaims(manager),
            expires: JwtTokenConfiguration.ExpirationDate,
            signingCredentials: jwtTokenConfiguration.Credentials);

        private static List<Claim> GetClaims(Manager manager) => new()
        {
            new(JwtRegisteredClaimNames.UniqueName, manager.UserName),
            new(ClaimTypes.Name, manager.UserName),
            new(ClaimTypes.Authentication, manager.UserName),
        };

        public JwtSecurityTokenFactory(JwtTokenConfiguration jwtTokenConfiguration) =>
            this.jwtTokenConfiguration = jwtTokenConfiguration;
    }
}
