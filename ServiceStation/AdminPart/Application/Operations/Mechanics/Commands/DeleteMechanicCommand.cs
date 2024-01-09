using Application.Interfaces;
using Domain.Entities;
using Domain.Exeptions;
using MediatR;

namespace Application.Operations.Mechanics.Commands
{
    public class DeleteMechanicCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteMechanicHandler : IRequestHandler<DeleteMechanicCommand>
        {
            private readonly IServiceStationDContext _context;

            public DeleteMechanicHandler(IServiceStationDContext context)
            {
                _context = context;
            }
            async Task IRequestHandler<DeleteMechanicCommand>.Handle(DeleteMechanicCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Mechanics
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Mechanic), request.Id);
                }

                _context.Mechanics.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

            }


        }
    }
}
