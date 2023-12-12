
using ServiceStation.DAL.Entities;

namespace ServiceStation.DAL.Repositories.Contracts
{
    public  interface IModelRepository : IGenericRepository<Model>
    {
         Task<Model> GetModelByName(string name);
    }

}
