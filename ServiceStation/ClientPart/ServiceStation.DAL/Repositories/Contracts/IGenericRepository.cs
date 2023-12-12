using System.Collections.Generic;
using System.Threading.Tasks;

namespace ServiceStation.DAL.Repositories.Contracts
{
    public interface IGenericRepository<TEntity> where TEntity : class
    {
        Task<IEnumerable<TEntity>> GetAsync();

        Task<TEntity> GetByIdAsync(int id);

        Task InsertAsync(TEntity entity);

        Task UpdateAsync(TEntity entity);

        Task DeleteAsync(int id);
    }
}
