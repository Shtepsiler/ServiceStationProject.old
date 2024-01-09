using ServiceStation.DAL.Entities;
using System.IdentityModel.Tokens.Jwt;

namespace ServiceStation.BLL.Services.Interfaces
{
    public interface ITokenService
    {
        string SerializeToken(JwtSecurityToken jwtToken);
        string GetRefreshToken(string username);
        //bool IsValid(JwtResponse response, out string username);
        string GetAccessTokenByRefreshToken(string refreshToken);
        JwtSecurityToken BuildToken(Client client);
        void DeleteRefreshToken(string clientName);

    }
}
