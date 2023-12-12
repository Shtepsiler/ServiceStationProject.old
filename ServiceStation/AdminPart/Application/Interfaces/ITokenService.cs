using Domain.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace Application.Interfaces
{
    public interface ITokenService
    {
        string SerializeToken(JwtSecurityToken jwtToken);
        JwtSecurityToken BuildToken(Manager client);

    }
}
