
using Application.Interfaces;
using Domain.Entities;
using Domain.Exeptions;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operations.Models.Commands;

public record UpdateModelCommand : IRequest
{
    public int Id { get; set; }
    public string Name { get; set; }

}

public class UpdateModelCommandHandler : IRequestHandler<UpdateModelCommand>
{
    private readonly IServiceStationDContext _context;

    public UpdateModelCommandHandler(IServiceStationDContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateModelCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Models
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Job), request.Id);
        }

        entity.Name = request.Name;


        await _context.SaveChangesAsync(cancellationToken);
    }
}
