using ServiceStation.BLL.DTO.Responses;
using ServiceStation.DAL.Entities;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

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
