using MassTransit;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeneralBusMessages.Message;
using Application.Interfaces;

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
            _context.Models.AddAsync(new(context.Message.Name ));
            _context.SaveChangesAsync();
            return Task.CompletedTask;
        }
    }
}
