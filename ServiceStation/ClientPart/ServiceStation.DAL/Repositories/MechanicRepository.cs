using ServiceStation.DAL.Data;
using ServiceStation.DAL.Entities;
using ServiceStation.DAL.Repositories.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ServiceStation.DAL.Repositories
{
    public class MechanicRepository : GenericRepository<Mechanic>, IMechanicRepository
    {
        public MechanicRepository(ServiceStationDContext databaseContext) : base(databaseContext)
        {
        }
    }
}
