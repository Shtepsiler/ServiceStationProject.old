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
    public class GetMechanicsQuery : IRequest<IEnumerable<MechanicDTO>>
    {
    }

    public class GetMechanicsQueryHendler : IRequestHandler<GetMechanicsQuery, IEnumerable<MechanicDTO>>
    {
        private readonly IServiceStationDContext _context;
        private readonly IMapper _mapper;

        public GetMechanicsQueryHendler(IServiceStationDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MechanicDTO>> Handle(GetMechanicsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<Mechanic>, IEnumerable<MechanicDTO>>(await _context.Mechanics.ToListAsync());
        }
    }



}
