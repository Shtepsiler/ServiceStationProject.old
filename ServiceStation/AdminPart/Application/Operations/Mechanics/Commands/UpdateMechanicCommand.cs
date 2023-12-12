
using Application.Interfaces;
using Domain.Entities;
using Domain.Exeptions;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operations.Mechanics.Commands;

public record UpdateMechanicCommand : IRequest
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? Address { get; set; }
    public string? Phone { get; set; }
    public string? Specialization { get; set; }
}

public class UpdateMechanicCommandHandler : IRequestHandler<UpdateMechanicCommand>
{
    private readonly IServiceStationDContext _context;

    public UpdateMechanicCommandHandler(IServiceStationDContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateMechanicCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Mechanics
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Mechanic), request.Id);
        }

        entity.FirstName = request.FirstName;
        entity.LastName = request.LastName;
        entity.Address = request.Address;
        entity.Phone = request.Phone;
        entity.Specialization = request.Specialization;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
