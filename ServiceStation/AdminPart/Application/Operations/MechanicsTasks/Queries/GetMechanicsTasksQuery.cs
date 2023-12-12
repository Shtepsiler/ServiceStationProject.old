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

namespace Application.Operations.MechanicsTasks.Queries
{
    public class GetMechanicsTasksQuery : IRequest<IEnumerable<MechanicsTasksDTO>>
    {
    }

    public class GetMechanicsTasksQueryHendler : IRequestHandler<GetMechanicsTasksQuery, IEnumerable<MechanicsTasksDTO>>
    {
        private readonly IServiceStationDContext _context;
        private readonly IMapper _mapper;

        public GetMechanicsTasksQueryHendler(IServiceStationDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MechanicsTasksDTO>> Handle(GetMechanicsTasksQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<Domain.Entities.MechanicsTasks>, IEnumerable<MechanicsTasksDTO>>(await _context.MechanicsTasks.ToListAsync());
        }
    }



}
