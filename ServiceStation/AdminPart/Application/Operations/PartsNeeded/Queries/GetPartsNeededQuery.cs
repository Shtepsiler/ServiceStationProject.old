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

namespace Application.Operations.PartsNeeded.Queries
{
    public class GetPartsNeededQuery : IRequest<IEnumerable<PartNeededDTO>>
    {
    }

    public class GetPartsNeededQueryHendler : IRequestHandler<GetPartsNeededQuery, IEnumerable<PartNeededDTO>>
    {
        private readonly IServiceStationDContext _context;
        private readonly IMapper _mapper;

        public GetPartsNeededQueryHendler(IServiceStationDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PartNeededDTO>> Handle(GetPartsNeededQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<PartNeeded>, IEnumerable<PartNeededDTO>>(await _context.PartsNeeded.ToListAsync());
        }
    }



}
