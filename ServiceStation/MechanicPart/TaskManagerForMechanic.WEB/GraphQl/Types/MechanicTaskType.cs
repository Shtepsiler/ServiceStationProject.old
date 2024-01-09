using TaskManagerForMechanic.DAL;
using TaskManagerForMechanic.DAL.Entitys;
using TaskManagerForMechanic.WEB.Extensions;
using TaskManagerForMechanic.WEB.GraphQl.DataLoader;

namespace TaskManagerForMechanic.WEB.GraphQl.Types
{
    public class MechanicTaskType : ObjectType<MechanicsTasks>
    {
        protected override void Configure(IObjectTypeDescriptor<MechanicsTasks> descriptor)
        {

            descriptor
                .ImplementsNode()
                .IdField(t => t.Id)
                .ResolveNode((ctx, id) => ctx.DataLoader<MechanicTaskByIdDataLoader>().LoadAsync(id, ctx.RequestAborted));

            descriptor
                .Field(t => t.Job)
            //    .UsePaging()
                .ResolveWith<MechanicTasksResolvers>(t => t.GetJobAsync(default!, default!, default!, default))
                .UseDbContext<TaskManagerDbContext>()
                .Name("job");


            descriptor
                .Field(t => t.Mechanic)
                //   .UsePaging()
                .ResolveWith<MechanicTasksResolvers>(t => t.GetMechanicAsync(default!, default!, default!, default))
                .UseDbContext<TaskManagerDbContext>()
                .Name("mechanic");






        }




        private class MechanicTasksResolvers
        {
            public async Task<Mechanic> GetMechanicAsync(
            [Parent] MechanicsTasks task,
            [ScopedService] TaskManagerDbContext dbContext,
            MechanicByIdDataloader mechanicById,
            CancellationToken cancellationToken)
            {
                return await mechanicById.LoadAsync(task.MechanicId, cancellationToken);
            }


            public async Task<Job> GetJobAsync(
            [Parent] MechanicsTasks task,
            [ScopedService] TaskManagerDbContext dbContext,
            JobByIdDataLoader jobById,
            CancellationToken cancellationToken)
            {
                return await jobById.LoadAsync(task.JobId.Value, cancellationToken);
            }






        }

    }
}
