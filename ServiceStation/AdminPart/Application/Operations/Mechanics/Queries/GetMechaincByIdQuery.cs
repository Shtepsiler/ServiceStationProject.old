using Application.DTOs.Respponces;
using Application.Interfaces;
using AutoMapper;
using Domain.Entities;
using MediatR;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Application.Operations.Mechanics.Queries
{
    public class GetMechaincByIdQuery : IRequest<MechanicDTO>
    {
        public int Id { get; set; }

    }
    public class GetMechaincByIdQueryHendler : IRequestHandler<GetMechaincByIdQuery, MechanicDTO>
    {
        private readonly IServiceStationDContext _context;
        private readonly IMapper mapper;

        public GetMechaincByIdQueryHendler(IServiceStationDContext context, IMapper mapper)
        {
            _context = context;
            this.mapper = mapper;
        }

        public async Task<MechanicDTO> Handle(GetMechaincByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                return mapper.Map<Mechanic, MechanicDTO>(await _context.Mechanics.FindAsync(request.Id, cancellationToken));
            }
            catch (Exception ex) { throw ex; }


        }
    }
}
