using Microsoft.EntityFrameworkCore;
using TaskManagerForMechanic.DAL;
using TaskManagerForMechanic.DAL.Entitys;
using TaskManagerForMechanic.WEB.Extensions;
using TaskManagerForMechanic.WEB.GraphQl.DataLoader;

namespace TaskManagerForMechanic.WEB.GraphQl.Types
{
    public class MechanicType : ObjectType<Mechanic>
    {
        protected override void Configure(IObjectTypeDescriptor<Mechanic> descriptor)
        {


            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<MechanicByIdDataloader>().LoadAsync(id, ctx.RequestAborted));


            descriptor
                .Field(t => t.Jobs)
              //  .UsePaging()
                .ResolveWith<MechanicResolvers>(t => t.GetJobsAsync(default!, default!, default!, default))
                .UseDbContext<TaskManagerDbContext>()
                .Name("job");

            descriptor
                .Field(t => t.Tasks)
                //.UsePaging()
                .ResolveWith<MechanicResolvers>(t => t.GeTasksAsync(default!, default!, default!, default))
                .UseDbContext<TaskManagerDbContext>()
                .Name("tasks");

        }




        private class MechanicResolvers
        {

            public async Task<IEnumerable<MechanicsTasks>> GeTasksAsync(
            [Parent] Mechanic mechanic,
            [ScopedService] TaskManagerDbContext dbContext,
            MechanicTaskByIdDataLoader tasksById,
            CancellationToken cancellationToken)
            {
                int[] tasksIds = await dbContext.MechanicsTasks
                    .Where(a => a.JobId == mechanic.Id)
                    .Select(p => p.Id)
                    .ToArrayAsync();

                return await tasksById.LoadAsync(tasksIds, cancellationToken);
            }
            public async Task<IEnumerable<Job>> GetJobsAsync(
            [Parent] Mechanic mechanic,
            [ScopedService] TaskManagerDbContext dbContext,
            JobByIdDataLoader jobsById,
            CancellationToken cancellationToken)
            {
                int[] jobIds = await dbContext.Jobs
                    .Where(a => a.MechanicId == mechanic.Id)
                    .Select(p => p.Id)
                    .ToArrayAsync();

                return await jobsById.LoadAsync(jobIds, cancellationToken);
            }









        }
    }
}
