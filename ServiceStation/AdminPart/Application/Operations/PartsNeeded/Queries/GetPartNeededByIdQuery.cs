using Application.DTOs.Respponces;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;

namespace Application.Operations.PartsNeeded.Queries
{
    public class GetPartNeededByIdQuery : IRequest<PartNeededDTO>
    {
        public int Id { get; set; }

    }
    public class GetPartNeededByIdQueryHendler : IRequestHandler<GetPartNeededByIdQuery, PartNeededDTO>
    {
        private readonly IServiceStationDContext _context;
        private readonly IMapper mapper;

        public GetPartNeededByIdQueryHendler(IServiceStationDContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public async Task<PartNeededDTO> Handle(GetPartNeededByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return mapper.Map<PartNeeded, PartNeededDTO>(await _context.PartsNeeded.FindAsync(request.Id, cancellationToken));
            }
            catch (Exception ex) { throw ex; }


        }
    }
}
