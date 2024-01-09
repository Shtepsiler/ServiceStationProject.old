﻿using Application.DTOs.Respponces;
using Application.Interfaces;
using AutoMapper;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Application.Operations.MechanicsTasks.Queries
{
    public class GetMechanicsTasksByParametrs : IRequest<IEnumerable<MechanicsTasksDTO>>
    {
        public int JobId { get; set; }
        public int MechanicId { get; set; }
    }

    public class GetMechanicsTasksByParametrsHendler : IRequestHandler<GetMechanicsTasksByParametrs, IEnumerable<MechanicsTasksDTO>>
    {
        private readonly IServiceStationDContext _context;
        private readonly IMapper _mapper;

        public GetMechanicsTasksByParametrsHendler(IServiceStationDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MechanicsTasksDTO>> Handle(GetMechanicsTasksByParametrs request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<Domain.Entities.MechanicsTasks>, IEnumerable<MechanicsTasksDTO>>(await _context.MechanicsTasks.Where(e => e.JobId == request.JobId && e.MechanicId == request.MechanicId).ToListAsync());
        }
    }


}
