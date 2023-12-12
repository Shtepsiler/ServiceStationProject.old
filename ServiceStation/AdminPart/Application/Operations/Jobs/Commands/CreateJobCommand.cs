
using Application.Interfaces;
using Application.Operations.Vendors.Commands;
using Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operations.Jobs.Commands;

public record CreateJobCommand : IRequest<int>
{
    /*    public int? ManagerId { get; set; }*/
    public int ModelId { get; set; }
    public int ClientId { get; set; }
    public DateTime IssueDate { get; set; }
    public string Description { get; set; }
}

public class CreateJobHandler : IRequestHandler<CreateJobCommand, int>
{
    private readonly IServiceStationDContext _context;

    public CreateJobHandler(IServiceStationDContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateJobCommand request, CancellationToken cancellationToken)
    {
        var entity = new Job()
        {
            /* ManagerId = request.ManagerId,*/
            ModelId = request.ModelId,
            ClientId = request.ClientId,
            IssueDate = request.IssueDate,
            Description = request.Description
        };

        await _context.Jobs.AddAsync(entity);


        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }

}
