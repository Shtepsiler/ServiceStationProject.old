using Microsoft.EntityFrameworkCore;
using ServiceStation.DAL.Data;
using ServiceStation.DAL.Entities;
using ServiceStation.DAL.Repositories.Contracts;
using System.Data;
using System.Data.SqlClient;

namespace ServiceStation.DAL.Repositories
{
    public class ModelRepository : GenericRepository<Model>, IModelRepository
    {
        public ModelRepository(ServiceStationDContext databaseContext)
            : base(databaseContext)
        {
        }


        public async Task<Model> GetModelByName(string name) => await table.Where(p => p.Name == name).FirstOrDefaultAsync();
    }
}
