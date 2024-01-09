using Application.Interfaces;
using Domain.Entities;
using Domain.Exeptions;
using MediatR;

namespace Application.Operations.Vendors.Commands
{
    public class DeleteVendorCommand : IRequest
    {
        public int Id { get; set; }

        public class DeleteVendorHandler : IRequestHandler<DeleteVendorCommand>
        {
            private readonly IServiceStationDContext _context;

            public DeleteVendorHandler(IServiceStationDContext context)
            {
                _context = context;
            }
            async Task IRequestHandler<DeleteVendorCommand>.Handle(DeleteVendorCommand request, CancellationToken cancellationToken)
            {
                var entity = await _context.Vendors
                    .FindAsync(request.Id);

                if (entity == null)
                {
                    throw new NotFoundException(nameof(Vendor), request.Id);
                }

                _context.Vendors.Remove(entity);

                await _context.SaveChangesAsync(cancellationToken);

            }



        }
    }
}
