using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ServiceStation.DAL.Data;
using ServiceStation.DAL.Repositories.Contracts;

namespace ServiceStation.DAL.Repositories
{
    public abstract class GenericRepository<TEntity> : IGenericRepository<TEntity> where TEntity : class
    {
        protected readonly ServiceStationDContext databaseContext;

        protected readonly DbSet<TEntity> table;

        public virtual async Task<IEnumerable<TEntity>> GetAsync() => await table.ToListAsync();

        public virtual async Task<TEntity> GetByIdAsync(int id)
        {
            return await table.FindAsync(id)
                ?? throw new Exception(
                    GetEntityNotFoundErrorMessage(id));
        }


        public virtual async Task InsertAsync(TEntity entity) => await table.AddAsync(entity);

        public virtual async Task UpdateAsync(TEntity entity) =>
            await Task.Run(() => table.Update(entity));

        public virtual async Task DeleteAsync(int id)
        {
            var entity = await GetByIdAsync(id);
            await Task.Run(() => table.Remove(entity));
        }

        protected static string GetEntityNotFoundErrorMessage(int id) =>
            $"{typeof(TEntity).Name} with id {id} not found.";

        public GenericRepository(ServiceStationDContext databaseContext)
        {
            this.databaseContext = databaseContext;
            table = this.databaseContext.Set<TEntity>();
        }
    }
}
