
using Application.Interfaces;
using Domain.Entities;
using Domain.Exeptions;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operations.OrderParts.Commands;

public record UpdateOrderPartCommand : IRequest
{
    public int Id { get; set; }
    public int OrderId { get; set; }

    public int PartId { get; set; }
    public int Quantity { get; set; }

}

public class UpdateOrderPartCommandHandler : IRequestHandler<UpdateOrderPartCommand>
{
    private readonly IServiceStationDContext _context;

    public UpdateOrderPartCommandHandler(IServiceStationDContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateOrderPartCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.OrderParts
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(OrderPart), request.Id);
        }

        entity.OrderId = request.OrderId;
        entity.PartId = request.PartId;
        entity.Quantity = request.Quantity;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
