using Application.Interfaces;
using Domain.Entities;
using Domain.Exeptions;
using MediatR;

namespace Application.Operations.Models.Commands
{
    public class DeleteModelCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteModelHandler : IRequestHandler<DeleteModelCommand>
        {
            private readonly IServiceStationDContext _context;

            public DeleteModelHandler(IServiceStationDContext context)
            {
                _context = context;
            }
            async Task IRequestHandler<DeleteModelCommand>.Handle(DeleteModelCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Models
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Model), request.Id);
                }

                _context.Models.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

            }



        }
    }
}
