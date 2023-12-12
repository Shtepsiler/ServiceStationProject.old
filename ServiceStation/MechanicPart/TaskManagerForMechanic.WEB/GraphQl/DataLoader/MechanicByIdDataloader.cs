using Microsoft.EntityFrameworkCore;
using TaskManagerForMechanic.DAL;
using TaskManagerForMechanic.DAL.Entitys;

namespace TaskManagerForMechanic.WEB.GraphQl.DataLoader
{

    public class MechanicByIdDataloader : BatchDataLoader<int, Mechanic>
    {
        private readonly IDbContextFactory<TaskManagerDbContext> _dbContextFactory;

        public MechanicByIdDataloader(IBatchScheduler batchScheduler, IDbContextFactory<TaskManagerDbContext> dbContextFactory):base(batchScheduler)
        {
            _dbContextFactory = dbContextFactory ??
                throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<int, Mechanic>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            await using TaskManagerDbContext dbContext =
                _dbContextFactory.CreateDbContext();

            return await dbContext.Mechanics.
                Where(p => keys.Contains(p.Id)).
                ToDictionaryAsync(t=>t.Id,cancellationToken);


        }
    }
}
