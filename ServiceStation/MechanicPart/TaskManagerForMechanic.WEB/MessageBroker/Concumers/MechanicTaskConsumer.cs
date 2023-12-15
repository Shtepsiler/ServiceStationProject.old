using GeneralBusMessages.Message;
using MassTransit;

namespace TaskManagerForMechanic.WEB.MessageBroker.Concumers
{
    public class MechanicTaskConsumer : IConsumer<GeneralBusMessages.Message.MechanicsTasks>
    {
        public Task Consume(ConsumeContext<MechanicsTasks> context)
        {
            throw new NotImplementedException();
        }
    }
}
