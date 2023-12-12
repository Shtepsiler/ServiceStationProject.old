using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operations.PartsNeeded.Commands;

public record CreatePartsNeededCommand : IRequest<int>
{
    public int JobId { get; set; }
    public int PartId { get; set; }
    public int Quantity { get; set; }
}

public class CreatePartNeededCommandHandler : IRequestHandler<CreatePartsNeededCommand, int>
{
    private readonly IServiceStationDContext _context;

    public CreatePartNeededCommandHandler(IServiceStationDContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreatePartsNeededCommand request, CancellationToken cancellationToken)
    {
        var entity = new PartNeeded()
        {
            JobId = request.JobId,
            PartId = request.PartId,
            Quantity = request.Quantity,
        };

        await _context.PartsNeeded.AddAsync(entity);


        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
