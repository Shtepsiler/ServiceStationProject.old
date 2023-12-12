using Microsoft.EntityFrameworkCore;
using TaskManagerForMechanic.DAL;
using TaskManagerForMechanic.DAL.Entitys;

namespace TaskManagerForMechanic.WEB.GraphQl.DataLoader
{
    public class JobByIdDataLoader : BatchDataLoader<int, Job>
    {
        private readonly IDbContextFactory<TaskManagerDbContext> _dbContextFactory;

        public JobByIdDataLoader(IBatchScheduler batchScheduler, IDbContextFactory<TaskManagerDbContext> dbContextFactory) : base(batchScheduler)
        {
            _dbContextFactory = dbContextFactory ??
                throw new ArgumentNullException(nameof(dbContextFactory));
        }

        protected override async Task<IReadOnlyDictionary<int, Job>> LoadBatchAsync(IReadOnlyList<int> keys, CancellationToken cancellationToken)
        {
            await using TaskManagerDbContext dbContext =
                _dbContextFactory.CreateDbContext();

            return await dbContext.Jobs.Where(p => keys.Contains(p.Id)).
                ToDictionaryAsync(t => t.Id, cancellationToken); 


        }
    }
}
