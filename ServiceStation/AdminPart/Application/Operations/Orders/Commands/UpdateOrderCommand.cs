
using Application.Interfaces;
using Domain.Entities;
using Domain.Exeptions;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operations.Orders.Commands;

public record UpdateOrderCommand : IRequest
{
    public int Id { get; set; }
    public int JobId { get; set; }
    public DateTime IssueDate { get; set; }
    public bool Delivered { get; set; }
    public bool IsOrdered { get; set; }

}

public class UpdateOrderCommandHandler : IRequestHandler<UpdateOrderCommand>
{
    private readonly IServiceStationDContext _context;

    public UpdateOrderCommandHandler(IServiceStationDContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = await _context.Orders
            .FindAsync(new object[] { request.Id }, cancellationToken);

        if (entity == null)
        {
            throw new NotFoundException(nameof(Job), request.Id);
        }
        entity.JobId = request.JobId;
        entity.IssueDate = request.IssueDate;
        entity.Delivered = request.Delivered;
        entity.IsOrdered = request.IsOrdered;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
