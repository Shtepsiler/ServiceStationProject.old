using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operations.Mechanics.Commands;

public record CreateMechanicCommand : IRequest<int>
{
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public string Address { get; set; }
    public string Phone { get; set; }
    public string Password { get; set; }
    public string Specialization { get; set; }
}

public class CreateMechanicCommandHandler : IRequestHandler<CreateMechanicCommand, int>
{
    private readonly IServiceStationDContext _context;

    public CreateMechanicCommandHandler(IServiceStationDContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateMechanicCommand request, CancellationToken cancellationToken)
    {
        var entity = new Mechanic()
        {
            FirstName = request.FirstName,
            LastName = request.LastName,
            Address = request.Address,
            Phone = request.Phone,
            Password = request.Password,
            Specialization = request.Specialization
        };

        await _context.Mechanics.AddAsync(entity);


        await _context.SaveChangesAsync(cancellationToken);

        return entity.Id;
    }
}
