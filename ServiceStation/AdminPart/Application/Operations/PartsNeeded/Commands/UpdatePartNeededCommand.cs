
using Application.Interfaces;
using Domain.Entities;
using Domain.Exeptions;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operations.PartsNeeded.Commands;

public record UpdatePartneededCommand : IRequest
{
    public int Id { get; set; }
    public int JobId { get; set; }
    public int PartId { get; set; }
    public int Quantity { get; set; }

}

public class UpdatePartneededCommandHandler : IRequestHandler<UpdatePartneededCommand>
{
    private readonly IServiceStationDContext _context;

    public UpdatePartneededCommandHandler(IServiceStationDContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdatePartneededCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.PartsNeeded
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Job), request.Id);
        }
        entity.JobId = request.JobId;
        entity.PartId = request.PartId;
        entity.Quantity = request.Quantity;



        await _context.SaveChangesAsync(cancellationToken);
    }
}
