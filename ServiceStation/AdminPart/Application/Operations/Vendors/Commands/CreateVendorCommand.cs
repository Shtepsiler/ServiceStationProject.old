using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operations.Vendors.Commands;

public record CreateVendorCommand : IRequest<int>
{
    public string Name { get; set; }
}

public class CreateJobCommandHandler : IRequestHandler<CreateVendorCommand, int>
{
    private readonly IServiceStationDContext _context;

    public CreateJobCommandHandler(IServiceStationDContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateVendorCommand request, CancellationToken cancellationToken)
    {
        var entity = new Vendor()
        {
            Name = request.Name
        };

        await _context.Vendors.AddAsync(entity);


        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
