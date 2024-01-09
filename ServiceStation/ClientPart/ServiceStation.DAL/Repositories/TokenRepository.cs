using Microsoft.EntityFrameworkCore;
using ServiceStation.DAL.Data;
using ServiceStation.DAL.Entities;
using ServiceStation.DAL.Repositories.Contracts;

namespace ServiceStation.DAL.Repositories
{
    public class TokenRepository : GenericRepository<RefreshToken>, ITokenRepository
    {

        public TokenRepository(ServiceStationDContext databaseContext)
    : base(databaseContext)
        {
        }

        public async Task<RefreshToken> GeTokenByClientName(string clientname) => await table.Where(p => p.ClientName == clientname).FirstOrDefaultAsync();

        public async Task DeleteTokenByClientName(string clientname)
        {
            try
            {
                var entity = await table.Where(p => p.ClientName == clientname).FirstOrDefaultAsync();
                await Task.Run(() => table.Remove(entity));
            }
            catch (Exception ex) { }
        }

        public async Task<RefreshToken> GeTokenByToken(string token) => await table.Where(p => p.ClientSecret == token).FirstOrDefaultAsync();





    }
}
