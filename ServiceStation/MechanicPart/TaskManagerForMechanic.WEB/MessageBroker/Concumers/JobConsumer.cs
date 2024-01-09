using GeneralBusMessages.Message;
using MassTransit;
using TaskManagerForMechanic.DAL;

namespace TaskManagerForMechanic.WEB.MessageBroker.Concumers
{
    public class JobConsumer : IConsumer<GeneralBusMessages.Message.Job>
    {
        private TaskManagerDbContext taskManagerDbContext;

        public JobConsumer(TaskManagerDbContext taskManagerDbContext)
        {
            this.taskManagerDbContext = taskManagerDbContext;
        }

        public Task Consume(ConsumeContext<Job> context)
        {
            taskManagerDbContext.Jobs.AddAsync(new()
            {
                ClientId = context.Message.ClientId,
                Description = context.Message.Description,
                FinishDate = context.Message.FinishDate,
                IssueDate = context.Message.IssueDate,
                ManagerId = context.Message.ManagerId,
                MechanicId = context.Message.MechanicId,
                ModelId = context.Message.ModelId,
                Price = context.Message.Price,
                Status = context.Message.Status
            });
            taskManagerDbContext.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
