
using Application.Interfaces;
using Domain.Entities;
using Domain.Exeptions;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operations.Vendors.Commands;

public record UpdateVendorCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }

}

public class UpdateVendorCommandHandler : IRequestHandler<UpdateVendorCommand>
{
    private readonly IServiceStationDContext _context;

    public UpdateVendorCommandHandler(IServiceStationDContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateVendorCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Vendors
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Job), request.Id);
        }

        entity.Name = request.Name;


        await _context.SaveChangesAsync(cancellationToken);
    }
}
