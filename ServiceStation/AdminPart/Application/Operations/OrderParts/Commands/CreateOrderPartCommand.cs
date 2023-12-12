using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operations.OrderParts.Commands;

public record CreateOrderPartCommand : IRequest<int>
{
    public int OrderId { get; set; }

    public int PartId { get; set; }
    public int Quantity { get; set; }

}

public class CreateOrderPartCommandHandler : IRequestHandler<CreateOrderPartCommand, int>
{
    private readonly IServiceStationDContext _context;

    public CreateOrderPartCommandHandler(IServiceStationDContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateOrderPartCommand request, CancellationToken cancellationToken)
    {
        var entity = new OrderPart()
        {
            OrderId = request.OrderId,
            PartId = request.PartId,
            Quantity = request.Quantity
        };

        await _context.OrderParts.AddAsync(entity);


        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
