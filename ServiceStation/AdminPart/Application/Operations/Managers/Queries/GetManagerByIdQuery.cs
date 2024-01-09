using Application.DTOs.Respponces;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Operations.Managers.Queries;

public class GetManagerByIdQuery : IRequest<ManagerDTO>
{
    public int Id { get; set; }

}
public class GetManagerByIdQueryHendler : IRequestHandler<GetManagerByIdQuery, ManagerDTO>
{
    private readonly IServiceStationDContext _context;
    private readonly IMapper mapper;

    public GetManagerByIdQueryHendler(IServiceStationDContext context, IMapper mapper)
    {
        _context = context;
        this.mapper = mapper;
    }

    public async Task<ManagerDTO> Handle(GetManagerByIdQuery request, CancellationToken cancellationToken)
    {
        try
        {
            return mapper.Map<Manager, ManagerDTO>(await _context.Managers.FindAsync(request.Id, cancellationToken));
        }
        catch (Exception ex) { throw ex; }


    }
}
