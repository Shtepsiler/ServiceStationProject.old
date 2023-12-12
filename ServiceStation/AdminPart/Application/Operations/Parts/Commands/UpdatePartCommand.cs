
using Application.Interfaces;
using Domain.Entities;
using Domain.Exeptions;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operations.Parts.Commands;

public record UpdatePartCommand : IRequest
{
    public int Id { get; set; }
    public string SerialNumber { get; set; }
    public string Description { get; set; }
    public decimal Price { get; set; }
    public int VendorId { get; set; }
    public int StockQty { get; set; }
}

public class UpdatePartCommandHandler : IRequestHandler<UpdatePartCommand>
{
    private readonly IServiceStationDContext _context;

    public UpdatePartCommandHandler(IServiceStationDContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdatePartCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Parts
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Job), request.Id);
        }
        entity.SerialNumber = request.SerialNumber;
        entity.Description = request.Description;
        entity.Price = request.Price;
        entity.VendorId = request.VendorId;
        entity.StockQty = request.StockQty;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
