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
    public class ManagerRepository : GenericRepository<Manager>, IManagerRepository
    {
        public ManagerRepository(ServiceStationDContext databaseContext)
            : base(databaseContext)
        {
        }
    }
}
