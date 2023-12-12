
using Application.Interfaces;
using Domain.Entities;
using Domain.Exeptions;
using MediatR;
using System.ComponentModel.DataAnnotations.Schema;

namespace Application.Operations.Managers.Commands;

public record UpdateManagerCommand : IRequest
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public string Phone { get; set; }
    public string Email { get; set; }
}

public class UpdateManagerCommandHandler : IRequestHandler<UpdateManagerCommand>
{
    private readonly IServiceStationDContext _context;

    public UpdateManagerCommandHandler(IServiceStationDContext context)
    {
        _context = context;
    }

    public async Task Handle(UpdateManagerCommand request, CancellationToken cancellationToken)
    {
        throw new NotImplementedException();
        /* var entity = await _context.Managers
             .FindAsync(new object[] { request.Id }, cancellationToken);

         if (entity == null)
         {
             throw new NotFoundException(nameof(Manager), request.Id);
         }

         entity.FullName = request.FullName;
         entity.PhoneNumber = request.Phone;
         entity.Email = request.Email;


         await _context.SaveChangesAsync(cancellationToken);*/
    }
}
