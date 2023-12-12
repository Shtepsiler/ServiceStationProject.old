using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operations.Parts.Commands;

public record CreatePartCommand : IRequest<int>
{
    public string SerialNumber { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int VendorId { get; set; }
    public int StockQty { get; set; }
}

public class CreatePartCommandHandler : IRequestHandler<CreatePartCommand, int>
{
    private readonly IServiceStationDContext _context;

    public CreatePartCommandHandler(IServiceStationDContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreatePartCommand request, CancellationToken cancellationToken)
    {
        var entity = new Part()
        {
            SerialNumber = request.SerialNumber,
            Description = request.Description,
            Price = request.Price,
            VendorId = request.VendorId,
            StockQty = request.StockQty
        };

        await _context.Parts.AddAsync(entity);


        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
