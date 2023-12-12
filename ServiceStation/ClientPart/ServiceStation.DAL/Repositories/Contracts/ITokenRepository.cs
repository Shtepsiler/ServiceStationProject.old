using ServiceStation.DAL.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.DAL.Repositories.Contracts
{
    public interface ITokenRepository : IGenericRepository<RefreshToken>
    {
        Task<RefreshToken> GeTokenByClientName(string clientname);
        Task DeleteTokenByClientName(string clientname);

        Task<RefreshToken> GeTokenByToken(string token);


    }
}
