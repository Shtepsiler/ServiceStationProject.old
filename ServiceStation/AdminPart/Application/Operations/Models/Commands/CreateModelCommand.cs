using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operations.Models.Commands
{
    public record CreateModelCommand : IRequest<int>
    {
        public string Name { get; set; }
    }

    public class CreateJobCommandHandler : IRequestHandler<CreateModelCommand, int>
    {
        private readonly IServiceStationDContext _context;
        public CreateJobCommandHandler(IServiceStationDContext context)
        {
            _context = context;
        }

        public async Task<int> Handle(CreateModelCommand request, CancellationToken cancellationToken)
        {
            var entity = new Model(request.Name);


            await _context.Models.AddAsync(entity);


            await _context.SaveChangesAsync(cancellationToken);

            return entity.Id;
        }
    }
}