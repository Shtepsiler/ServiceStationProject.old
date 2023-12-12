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

namespace Application.Operations.Parts.Queries
{
    public class GetPartsQuery : IRequest<IEnumerable<PartDTO>>
    {
    }

    public class GetPartsQueryHendler : IRequestHandler<GetPartsQuery, IEnumerable<PartDTO>>
    {
        private readonly IServiceStationDContext _context;
        private readonly IMapper _mapper;

        public GetPartsQueryHendler(IServiceStationDContext context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public async Task<IEnumerable<PartDTO>> Handle(GetPartsQuery request, CancellationToken cancellationToken)
        {
            return _mapper.Map<IEnumerable<Part>, IEnumerable<PartDTO>>(await _context.Parts.ToListAsync());
        }
    }



}
