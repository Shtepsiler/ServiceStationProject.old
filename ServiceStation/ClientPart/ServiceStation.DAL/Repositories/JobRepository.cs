using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceStation.DAL.Data;
using ServiceStation.DAL.Entities;
using ServiceStation.DAL.Repositories.Contracts;

namespace ServiceStation.DAL.Repositories
{
    public class JobRepository : GenericRepository<Job>, IJobRepository
    {
        public JobRepository(ServiceStationDContext databaseContext)
            : base(databaseContext)
        {
        }

        public async Task<IEnumerable<Job>> GetByClientIdAsync(int id)
        {
            return await table.Where(p=>p.ClientId == id).Include(p=>p.Model).Include(p=>p.Manager).Include(p=>p.Mechanic).ToListAsync()
                ?? throw new Exception(
                    GetEntityNotFoundErrorMessage(id));
        }






    }
}
