using ServiceStation.DAL.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace ServiceStation.BLL.Factories.Interfaces
{
    public interface IJwtSecurityTokenFactory
    {
        JwtSecurityToken BuildToken(Client client);
    }
}
