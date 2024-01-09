using ServiceStation.DAL.Data;
using ServiceStation.DAL.Entities;
using ServiceStation.DAL.Repositories.Contracts;

namespace ServiceStation.DAL.Repositories
{
    public class MechanicRepository : GenericRepository<Mechanic>, IMechanicRepository
    {
        public MechanicRepository(ServiceStationDContext databaseContext) : base(databaseContext)
        {
        }
    }
}
