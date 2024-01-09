using Application.Interfaces;
using Domain.Entities;
using Domain.Exeptions;
using MediatR;

namespace Application.Operations.Managers.Commands;

public class DeleteManagerCommand : IRequest
{
    public int Id { get; set; }

    public class DeleteManagerHandler : IRequestHandler<DeleteManagerCommand>
    {
        private readonly IServiceStationDContext _context;

        public DeleteManagerHandler(IServiceStationDContext context)
        {
            _context = context;
        }
        async Task IRequestHandler<DeleteManagerCommand>.Handle(DeleteManagerCommand request, CancellationToken cancellationToken)
        {
            var entity = await _context.Managers
                .FindAsync(request.Id);

            if (entity == null)
            {
                throw new NotFoundException(nameof(Manager), request.Id);
            }

            _context.Managers.Remove(entity);

            await _context.SaveChangesAsync(cancellationToken);

        }



    }
}
