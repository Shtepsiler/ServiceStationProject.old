using Application.DTOs.Respponces;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exeptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Operations.PartsNeeded.Commands
{
    public class DeletePartNeededCommand : IRequest
    {
        public int Id { get; set; }

        public class DeletePartNeededHandler : IRequestHandler<DeletePartNeededCommand>
        {
            private readonly IServiceStationDContext _context;

            public DeletePartNeededHandler(IServiceStationDContext context)
            {
                _context = context;
            }
            async Task IRequestHandler<DeletePartNeededCommand>.Handle(DeletePartNeededCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.PartsNeeded
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(PartNeeded), request.Id);
                }

                _context.PartsNeeded.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

            }



        }
    }
}
