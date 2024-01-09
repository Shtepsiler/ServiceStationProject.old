using Microsoft.EntityFrameworkCore;
using TaskManagerForMechanic.DAL;
using TaskManagerForMechanic.DAL.Entitys;
using TaskManagerForMechanic.WEB.Extensions;
using TaskManagerForMechanic.WEB.GraphQl.DataLoader;

namespace TaskManagerForMechanic.WEB.GraphQl.Types
{
    public class JobType : ObjectType<Job>
    {
        protected override void Configure(IObjectTypeDescriptor<Job> descriptor)
        {

            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<JobByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(t => t.Mechanic)
                .ResolveWith<JobResolvers>(t => t.GetMechanicAsync(default!, default!, default!, default))
                .UseDbContext<TaskManagerDbContext>()
                .Name("mechanic");

            descriptor
                .Field(t => t.Tasks)
                .UsePaging(options: new()
                {
                    MaxPageSize = 4,
                    DefaultPageSize = 4,


                })
                .ResolveWith<JobResolvers>(t => t.GeTasksAsync(default!, default!, default!, default))
                .UseDbContext<TaskManagerDbContext>()
                .Name("tasks");

        }




        private class JobResolvers
        {
            public async Task<Mechanic> GetMechanicAsync(
            [Parent] Job job,
            [ScopedService] TaskManagerDbContext dbContext,
            MechanicByIdDataloader mechanicById,
            CancellationToken cancellationToken)
            {
                return await mechanicById.LoadAsync(job.MechanicId.Value, cancellationToken);
            }

            public async Task<IEnumerable<MechanicsTasks>> GeTasksAsync(
            [Parent] Job job,
            [ScopedService] TaskManagerDbContext dbContext,
            MechanicTaskByIdDataLoader tasksById,
            CancellationToken cancellationToken)
            {
                int[] tasksIds = await dbContext.MechanicsTasks
                    .Where(a => a.JobId == job.Id)
                    .Select(p => p.Id)
                    .ToArrayAsync();

                return await tasksById.LoadAsync(tasksIds, cancellationToken);
            }









        }
    }
}
