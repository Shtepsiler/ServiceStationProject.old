using AutoMapper;
using ServiceStation.BLL.DTO.Requests;
using ServiceStation.BLL.DTO.Responses;
using ServiceStation.BLL.Services.Interfaces;
using ServiceStation.DAL.Entities;
using ServiceStation.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.BLL.Services
{
    public class ClientService : IClientService
    {
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper mapper; 
        private readonly ITokenService tokenService;

        public ClientService(IUnitOfWork unitOfWork, IMapper mapper, ITokenService tokenService)
        {
            this.unitOfWork = unitOfWork;
            this.mapper = mapper;
            this.tokenService = tokenService;
        }

        public async Task RewokeRefreshToken(string clientMame,string token)
        {
            try
            {
                
                var tok = unitOfWork._TokenRepository.GeTokenByClientName(clientMame);
                if (tok.Result.ClientSecret==token)
                tok.Result.ExpirationDate = DateTime.Now.AddDays(1);
                else
               throw new UnauthorizedAccessException("No valid refresh token");
            }
            catch(Exception ex) { throw ex; }


        }

        public async Task<ClientResponse> GetClientByName(string name)
        {
            try
            {
                return mapper.Map<Client, ClientResponse>(await unitOfWork._ClientManager.FindByNameAsync(name));
            }
            catch (Exception ex) { throw ex; }

        }

        public async Task<JwtResponse> RenewAccesToken(string refreshtoken)
        {
            var tok = await unitOfWork._TokenRepository.GeTokenByToken(refreshtoken);

            if (tok is null) throw new Exception();

            if (tok.ExpirationDate < DateTime.Now)
            {

                await unitOfWork._TokenRepository.DeleteAsync(tok.Id);
                await unitOfWork.SaveChangesAsync();

                throw new Exception("ExpirationDate is expirated, it will be deleted");
            }



            var user = await unitOfWork._ClientManager.FindByNameAsync(tok.ClientName);

            var jwtToken = tokenService.BuildToken(user);

            return new() { Id = user.Id, Token = tokenService.SerializeToken(jwtToken), ClientName = user.UserName, RefreshToken = tokenService.GetRefreshToken(user.UserName) };



        }

        public async Task UpdateAsync(string name, ClientRequest client)
        {
     var user = await unitOfWork._ClientManager.FindByNameAsync(name);
            if (user == null) throw new Exception();
            user.UserName = client.ClientName;
            user.FirstName = client.FirstName;
            user.LastName = client.LastName;
            user.PhoneNumber = client.PhoneNumber;
            user.Email = client.Email;



            await unitOfWork._ClientManager.UpdateAsync(user);
            
            await unitOfWork.SaveChangesAsync();

        }

        public async Task DeleteAsync(string name)
        {
            var user = await unitOfWork._ClientManager.FindByNameAsync(name);
            if (user == null) throw new Exception();

            await unitOfWork._ClientManager.DeleteAsync(user);
            await unitOfWork.SaveChangesAsync();
        }
    }

}
