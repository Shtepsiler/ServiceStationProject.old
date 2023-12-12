using Application.Factories.Interfaces;
using Application.Interfaces;
using Domain.Entities;
using Microsoft.Extensions.Configuration;
using System.IdentityModel.Tokens.Jwt;

namespace Infrastructure.Persistence.Services
{
    public class TokenService : ITokenService
    {
        private readonly IConfiguration configuration;
        private readonly IJwtSecurityTokenFactory tokenFactory;


        public TokenService(IJwtSecurityTokenFactory tokenFactory, IConfiguration configuration)
        {
            this.configuration = configuration;
            this.tokenFactory = tokenFactory;
        }
        public string SerializeToken(JwtSecurityToken jwtToken) =>
    new JwtSecurityTokenHandler().WriteToken(jwtToken);

        public JwtSecurityToken BuildToken(Manager manager) => tokenFactory.BuildToken(manager);













    }
}
