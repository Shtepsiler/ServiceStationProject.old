
using Application.Interfaces;
using Domain.Entities;
using Domain.Exeptions;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operations.MechanicsTasks.Commands;

public record UpdateMechanicTaskCommand : IRequest
{
    public int Id { get; set; }
    public int MechanicId { get; set; }
    public int? JobId { get; set; }
    public string Task { get; set; }
}

public class UpdateMechanicTaskCommandHandler : IRequestHandler<UpdateMechanicTaskCommand>
{
    private readonly IServiceStationDContext _context;

    public UpdateMechanicTaskCommandHandler(IServiceStationDContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateMechanicTaskCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.MechanicsTasks
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Domain.Entities.MechanicsTasks), request.Id);
        }
        entity.MechanicId = request.MechanicId;
        entity.JobId = request.JobId;
        entity.Task = request.Task;


        await _context.SaveChangesAsync(cancellationToken);
    }
}
