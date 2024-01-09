using ServiceStation.DAL.Entities;

namespace ServiceStation.DAL.Repositories.Contracts
{
    public interface ITokenRepository : IGenericRepository<RefreshToken>
    {
        Task<RefreshToken> GeTokenByClientName(string clientname);
        Task DeleteTokenByClientName(string clientname);

        Task<RefreshToken> GeTokenByToken(string token);


    }
}
