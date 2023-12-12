using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operations.Orders.Commands;

public record CreateOrderCommand : IRequest<int>
{
    public int JobId { get; set; }
    public DateTime IssueDate { get; set; }
    public bool Delivered { get; set; }
    public bool IsOrdered { get; set; }
}

public class CreateJobCommandHandler : IRequestHandler<CreateOrderCommand, int>
{
    private readonly IServiceStationDContext _context;

    public CreateJobCommandHandler(IServiceStationDContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateOrderCommand request, CancellationToken cancellationToken)
    {
        var entity = new Order()
        {
            JobId = request.JobId,
            IssueDate = request.IssueDate,
            Delivered = request.Delivered,
            IsOrdered = request.IsOrdered,
        };

        await _context.Orders.AddAsync(entity);


        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
