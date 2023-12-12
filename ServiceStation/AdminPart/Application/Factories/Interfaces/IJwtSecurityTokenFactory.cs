using Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Factories.Interfaces
{
    public interface IJwtSecurityTokenFactory
    {
        JwtSecurityToken BuildToken(Manager client);
    }
}
