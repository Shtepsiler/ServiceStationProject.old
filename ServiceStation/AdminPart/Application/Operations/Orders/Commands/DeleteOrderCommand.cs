using Application.DTOs.Respponces;
using Application.Interfaces;
using Domain.Entities;
using Domain.Exeptions;
using MediatR;
using System.Threading;
using System.Threading.Tasks;

namespace Application.Operations.Orders.Commands
{
    public class DeleteOrderCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteOrderHandler : IRequestHandler<DeleteOrderCommand>
        {
            private readonly IServiceStationDContext _context;

            public DeleteOrderHandler(IServiceStationDContext context)
            {
                _context = context;
            }
            async Task IRequestHandler<DeleteOrderCommand>.Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Orders
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Order), request.Id);
                }

                _context.Orders.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

            }



        }
    }
}
