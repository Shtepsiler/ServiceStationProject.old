using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operations.MechanicsTasks.Commands;

public record CreateMechanicTaskCommand : IRequest<int>
{
    public int MechanicId { get; set; }
    public int? JobId { get; set; }
    public string Task { get; set; }
    public string Status { get; set; }
}

public class CreateMechanicTaskCommandHandler : IRequestHandler<CreateMechanicTaskCommand, int>
{
    private readonly IServiceStationDContext _context;

    public CreateMechanicTaskCommandHandler(IServiceStationDContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateMechanicTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = new Domain.Entities.MechanicsTasks()
        {
            MechanicId = request.MechanicId,
            JobId = request.JobId,
            Task = request.Task,
            Status = request.Status,

        };

        await _context.MechanicsTasks.AddAsync(entity);


        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
