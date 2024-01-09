using Application.Interfaces;
using GeneralBusMessages.Message;
using MassTransit;

namespace Application.EventBusConsumers
{
    public class ModelConsumer : IConsumer<Model>
    {
        private readonly IServiceStationDContext _context;

        public ModelConsumer(IServiceStationDContext context)
        {
            _context = context;
        }

        public Task Consume(ConsumeContext<Model> context)
        {
            _context.Models.AddAsync(new(context.Message.Name));
            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
