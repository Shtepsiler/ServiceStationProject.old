using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Primitives;
using Microsoft.IdentityModel.Tokens;
using ServiceStation.BLL.DTO.Responses;
using ServiceStation.BLL.Factories.Interfaces;
using ServiceStation.BLL.Services.Interfaces;
using ServiceStation.DAL.Entities;
using ServiceStation.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.BLL.Services
{
    public class TokenService : ITokenService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IConfiguration configuration; 
        private readonly IJwtSecurityTokenFactory tokenFactory;


        public TokenService(IUnitOfWork unitOfWork, IJwtSecurityTokenFactory tokenFactory, IConfiguration configuration)
        {
            this.unitOfWork = unitOfWork;
            this.configuration = configuration;
            this.tokenFactory = tokenFactory;
        }
        public string SerializeToken(JwtSecurityToken jwtToken) =>
    new JwtSecurityTokenHandler().WriteToken(jwtToken);

       public JwtSecurityToken BuildToken(Client client) => tokenFactory.BuildToken(client);



        public string GetAccessTokenByRefreshToken(string refreshToken)
        {

            try
            {       
               var token = unitOfWork._TokenRepository.GeTokenByToken(refreshToken);

                if (token.Result == null) {

                    throw new UnauthorizedAccessException("token is not to be)");
                }
                if(token.Result.ExpirationDate <= DateTime.Now)
                {
                    unitOfWork._TokenRepository.DeleteTokenByClientName(token.Result.ClientName);
                    unitOfWork.SaveChangesAsync();
                    throw new UnauthorizedAccessException("Refresh Token is expired,it will be deleted");
                }

              var client  = unitOfWork._ClientManager.FindByNameAsync(token.Result.ClientName);

                if (client.Result == null) {
                    throw new NullReferenceException($"{nameof(client)} is not excist");
                }

                var jwtSecutity = BuildToken(client.Result);
                return SerializeToken(jwtSecutity);





            }
            catch (Exception ex) { throw ex; }

        }



        public void DeleteRefreshToken(string clientName)
        {
            try
            {
                unitOfWork._TokenRepository.DeleteTokenByClientName(clientName);
                unitOfWork.SaveChangesAsync();
            }
            catch(Exception ex) { throw ex; }   

        }


        public string GetRefreshToken(string username)
        {
            try
            {
                var ifrexisttoken = unitOfWork._TokenRepository.GeTokenByClientName(username);
                if (ifrexisttoken.Result == null)
                {
                    var newguid = Guid.NewGuid();
                    unitOfWork._TokenRepository.InsertAsync(new RefreshToken { ClientName = username, ClientSecret = newguid.ToString(), ExpirationDate = DateTime.Now.AddDays(1) });
                    unitOfWork.SaveChangesAsync();
                    return newguid.ToString();

                }
                
                if (ifrexisttoken.Result.ExpirationDate<DateTime.Now)
                {
                    DeleteRefreshToken(username);

                throw new Exception("Token is Expirationed it will be deleted you must login again");


                }
                    return ifrexisttoken.Result.ClientSecret;

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }
    


        /*
        public bool IsValid(JwtResponse response, out string username)
        {
            username = string.Empty;
            ClaimsPrincipal principal = GetPrincipalFromExpiredToken(response.Token);
            if (principal is null)
            {
                throw new UnauthorizedAccessException("No principal");
            }

            username = principal.FindFirstValue(ClaimTypes.Name);

            if (string.IsNullOrEmpty(username))
            {
                throw new UnauthorizedAccessException("No user name");
            }

            if (!Guid.TryParse(response.RefreshToken, out Guid givenRefreshToken))
            {
                throw new UnauthorizedAccessException("Refresh token malformed");
            }
            var curenttoken = unitOfWork._TokenRepository.GeTokenByClientName(response.ClientName);
            Guid curentRefreshToken = Guid.Parse(curenttoken.Result.ClientSecret);

            if (curenttoken.Result.ExpirationDate >= DateTime.Now)
            {
                unitOfWork._TokenRepository.DeleteTokenByClientName(username);
                unitOfWork.SaveChangesAsync();
                throw new UnauthorizedAccessException("Refresh Token is expired,it will be deleted");

            }

            if (curentRefreshToken == null)
            {
                throw new UnauthorizedAccessException("No valid refresh token in system");

            }
            if (curentRefreshToken != givenRefreshToken)
            {
                throw new UnauthorizedAccessException("invalid refresh token");
            }

            return true;

        }
        
  
        private ClaimsPrincipal GetPrincipalFromExpiredToken(string token)
        {
            TokenValidationParameters tokenValidationParameters = new()
            {
                ValidateIssuer = false,
                ValidateAudience = false,
                ValidateLifetime = false,
                ValidateIssuerSigningKey = true,
                IssuerSigningKey = new SymmetricSecurityKey(
                                 Encoding.UTF8.GetBytes(configuration["JwtSecurityKey"])),



            };
            
            JwtSecurityTokenHandler handler = new JwtSecurityTokenHandler();

            ClaimsPrincipal claimsPrincipal = handler.ValidateToken(
                token, tokenValidationParameters, out SecurityToken securityToken);

            if (securityToken is not JwtSecurityToken jwtSecurityToken || !jwtSecurityToken.Header.Alg.Equals(
                SecurityAlgorithms.HmacSha256,
                StringComparison.InvariantCulture))
            {
                throw new SecurityTokenException("invalid token");
            }


            return claimsPrincipal;

        }
        */






    }
}
