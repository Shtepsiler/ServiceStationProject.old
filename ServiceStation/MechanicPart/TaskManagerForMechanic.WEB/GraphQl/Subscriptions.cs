using TaskManagerForMechanic.DAL.Entitys;
using TaskManagerForMechanic.WEB.GraphQl.DataLoader;

namespace TaskManagerForMechanic.WEB.GraphQl
{
    public class Subscriptions
    {
        [Subscribe]
        [Topic]
        public Task<Job> OnJobStatusUpdateAsync(
            [EventMessage] int jobId,
            JobByIdDataLoader lobById,
            CancellationToken cancellationToken) =>
            lobById.LoadAsync(jobId, cancellationToken);



        [Subscribe]
        [Topic]
        public Task<MechanicsTasks> OnTaskStatusUpdateAsync(
            [EventMessage] int taskid,
            MechanicTaskByIdDataLoader TaskById,
            CancellationToken cancellationToken) =>
            TaskById.LoadAsync(taskid, cancellationToken);







    }
}
