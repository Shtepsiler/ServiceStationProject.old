using ServiceStation.BLL.DTO.Requests;
using ServiceStation.BLL.DTO.Responses;

namespace ServiceStation.BLL.Services.Interfaces
{
    public interface IClientService
    {
        // Task AddPhoneNumber();


        Task<ClientResponse> GetClientByName(string name);
        Task RewokeRefreshToken(string clientname, string token);

        Task<JwtResponse> RenewAccesToken(string refreshtoken);

        Task UpdateAsync(string name, ClientRequest client);
        Task DeleteAsync(string name);




    }
}
