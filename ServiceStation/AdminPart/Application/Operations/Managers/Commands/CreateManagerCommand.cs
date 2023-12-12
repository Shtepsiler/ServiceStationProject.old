using Application.Interfaces;
using Domain.Entities;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operations.Managers.Commands;

public record CreateManagerCommand : IRequest<int>
{
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }

    public string Password { get; set; }

}

public class CreateManagerCommandHandler : IRequestHandler<CreateManagerCommand, int>
{
    private readonly IServiceStationDContext _context;

    public CreateManagerCommandHandler(IServiceStationDContext context)
    {
        _context = context;
    }

    public async Task<int> Handle(CreateManagerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        /* var entity = new Manager()
         {
           FullName = request.FullName,
           PhoneNumber = request.Phone,
           Email = request.Email,
           Password = request.Password
         };

         await _context.Managers.AddAsync(entity);


         await _context.SaveChangesAsync(cancellationToken);

         return entity.Id;*/
    }
}
