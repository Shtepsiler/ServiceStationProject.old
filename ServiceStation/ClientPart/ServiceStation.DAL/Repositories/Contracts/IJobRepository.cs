


using ServiceStation.DAL.Entities;

namespace ServiceStation.DAL.Repositories.Contracts
{
    public interface IJobRepository : IGenericRepository<Job>
    {
        Task<IEnumerable<Job>> GetByClientIdAsync(int id);

    }

}
