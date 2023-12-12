using Application.Interfaces;
using Domain.Entities;
using Domain.Exeptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Operations.Jobs.Commands
{
    public class DeleteJobCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteCategoryJobHandler : IRequestHandler<DeleteJobCommand>
        {
            private readonly IServiceStationDContext _context;

            public DeleteCategoryJobHandler(IServiceStationDContext context)
            {
                _context = context;
            }
            async Task IRequestHandler<DeleteJobCommand>.Handle(DeleteJobCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Jobs
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Job), request.Id);
                }

                _context.Jobs.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

            }


        }
    }
}