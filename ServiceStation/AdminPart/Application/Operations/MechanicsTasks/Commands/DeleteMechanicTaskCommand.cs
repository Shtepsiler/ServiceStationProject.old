using Application.Interfaces;
using Domain.Exeptions;
using MediatR;

namespace Application.Operations.MechanicsTasks.Commands
{
    public class DeleteMechanicTaskCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteMechanicTaskHandler : IRequestHandler<DeleteMechanicTaskCommand>
        {
            private readonly IServiceStationDContext _context;

            public DeleteMechanicTaskHandler(IServiceStationDContext context)
            {
                _context = context;
            }
            async Task IRequestHandler<DeleteMechanicTaskCommand>.Handle(DeleteMechanicTaskCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.MechanicsTasks
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Domain.Entities.MechanicsTasks), request.Id);
                }

                _context.MechanicsTasks.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

            }


        }
    }
}
